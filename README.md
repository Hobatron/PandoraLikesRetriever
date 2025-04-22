# Pandora Likes Retriever

A simple C# console application that downloads your liked/thumbs-up songs from Pandora and saves them in an easy-to-read format.

## Features

- Downloads all your liked songs from Pandora
- Creates two output files:
  - `pandora_songs.txt`: Simple list of "Song Title by Artist Name"
  - `pandora_response.json`: Full details including album art, track IDs, etc.
- No API key needed - works with your browser session
- Self-contained executable - no .NET installation required

## How to Use

1. Download the latest release
2. Get your Pandora session info:
   - Go to pandora.com and log in
   - Open Developer Tools (F12 or Ctrl+Shift+I)
   - Go to the Network tab
   - Click on your profile/thumbs page
   - Look for a request to 'getFeedback'
   - In the request headers, find:
     - X-AuthToken
     - X-CsrfToken
     - Cookie
3. Run the executable
4. Enter the requested information
5. Your songs will be saved to:
   - `pandora_songs.txt` - Simple list of songs
   - `pandora_response.json` - Full details

## Building from Source

```bash
# Clone the repository
git clone https://github.com/yourusername/PandoraLikesRetriever.git

# Navigate to the project directory
cd PandoraLikesRetriever

# Build a self-contained executable
dotnet publish -c Release
```

The executable will be in `bin/Release/net8.0/win-x64/publish/PandoraLikesRetriever.exe`

## Requirements

- Windows operating system
- Active Pandora account with web session

## Notes

- This tool uses your active Pandora web session, so you need to be logged into Pandora in your browser
- The tokens expire after some time, so you'll need to get fresh ones if they expire
- This is an unofficial tool and not affiliated with Pandora

## License

MIT License - Feel free to use and modify as needed!
