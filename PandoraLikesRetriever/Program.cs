using System.Net.Http.Headers;
using System.Text.Json;
using PandoraLikesRetriever;

Console.WriteLine("Welcome to Pandora Likes Retriever!");
Console.WriteLine("\nTo get the required information:");
Console.WriteLine("1. Go to pandora.com and log in");
Console.WriteLine("2. Open Developer Tools (F12 or Ctrl+Shift+I)");
Console.WriteLine("3. Go to the Network tab");
Console.WriteLine("4. Click on your profile/thumbs page");
Console.WriteLine("5. Look for a request to 'getFeedback'");
Console.WriteLine("6. In the request headers, find X-AuthToken, X-CsrfToken, and Cookie under 'Request Headers'");

Console.WriteLine("\nPlease provide the following information from your Pandora web session:");

Console.Write("\nEnter your Pandora account name: ");
string accountName = Console.ReadLine() ?? string.Empty;

Console.Write("\nEnter your Cookie header value: ");
string cookieHeader = Console.ReadLine() ?? string.Empty;

Console.Write("\nEnter your X-AuthToken: ");
string authToken = Console.ReadLine() ?? string.Empty;

Console.Write("\nEnter your X-CsrfToken: ");
string csrfToken = Console.ReadLine() ?? string.Empty;


if (string.IsNullOrWhiteSpace(accountName) || 
    string.IsNullOrWhiteSpace(authToken) || 
    string.IsNullOrWhiteSpace(csrfToken) || 
    string.IsNullOrWhiteSpace(cookieHeader))
{
    Console.WriteLine("\nError: All fields are required. Please try again.");
    return;
}

string outputFile = "pandora_response.json";

using var client = new HttpClient();

// Add all required headers
client.DefaultRequestHeaders.Add("X-AuthToken", authToken);
client.DefaultRequestHeaders.Add("X-CsrfToken", csrfToken);
client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
client.DefaultRequestHeaders.Add("Origin", "https://www.pandora.com");
client.DefaultRequestHeaders.Add("Referer", $"https://www.pandora.com/profile/thumbs/{accountName}");
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/135.0.0.0 Safari/537.36");
client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"135\", \"Not-A.Brand\";v=\"8\", \"Chromium\";v=\"135\"");
client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
client.DefaultRequestHeaders.Add("Cookie", cookieHeader);

const int pageSize = 50;
var allFeedback = new List<FeedbackItem>();
var total = 0;
var startIndex = 0;

try
{
    do
    {
        var requestBody = new
        {
            pageSize,
            startIndex,
            webname = accountName
        };

        var json = JsonSerializer.Serialize(requestBody);
        var requestContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        Console.WriteLine($"Fetching items {startIndex} to {startIndex + pageSize}...");
        var response = await client.PostAsync("https://www.pandora.com/api/v1/station/getFeedback", requestContent);
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var feedbackResponse = JsonSerializer.Deserialize<PandoraFeedbackResponse>(responseContent, JsonContext.Default.PandoraFeedbackResponse);

        if (feedbackResponse == null) break;
        if (feedbackResponse.Feedback == null) break;
        
        allFeedback.AddRange(feedbackResponse.Feedback);
        total = feedbackResponse.Total;
        startIndex += pageSize;
        
        Console.WriteLine($"Retrieved {allFeedback.Count} of {total} items");
        
        // Add a small delay to avoid rate limiting
        await Task.Delay(500);
    } while (startIndex < total);

    var fullResponse = new PandoraFeedbackResponse
    {
        Total = total,
        Feedback = allFeedback
    };
    
    Console.WriteLine($"\nRetrieved {allFeedback.Count} total liked songs!");

    // Save all feedback items to file
    var formattedJson = JsonSerializer.Serialize(fullResponse, JsonContext.Default.PandoraFeedbackResponse);
    await File.WriteAllTextAsync(outputFile, formattedJson);
    Console.WriteLine($"\nFull response (including album art, track IDs, etc) written to: {outputFile}");

    // Create simple text output with just songs and artists
    var simpleSongList = allFeedback.Select(f => $"{f.SongTitle} by {f.ArtistName}");
    await File.WriteAllLinesAsync("pandora_songs.txt", simpleSongList);
    Console.WriteLine($"Simple song list (titles and artists only) written to: pandora_songs.txt");

    Console.WriteLine("\nDone! You can now close this window.");
}
catch (HttpRequestException ex)
{
    Console.WriteLine("\nError making request to Pandora!");
    Console.WriteLine("This usually means one of the following:");
    Console.WriteLine("1. One of the tokens (Auth/Csrf/Cookie) is incorrect");
    Console.WriteLine("2. Your account name is incorrect");
    Console.WriteLine("3. You're not logged into Pandora in your browser");
    Console.WriteLine("\nTechnical details:");
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("\nAn unexpected error occurred!");
    Console.WriteLine("Please try running the program again.");
    Console.WriteLine("\nTechnical details:");
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
}
