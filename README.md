#wrappify
###### client wrapper to connect Spotify web api

create new instance

    ISpotify spotify = new Spotify();
    ISpotify spotify = new Spotify(Scheme, Host, Port);

if you need to access personal data, you have to set and access token

    spotify.SetAccessToken("accecc_token here");

call method

    User user = await spotify.GetCurrentUserProfileTest();
    
    User user = await spotify.GetUser("username here");
    
    Paging<Playlist> playlistPaging = await spotify.GetAUsersPlaylists("username here");
    
    Track track = await spotify.GetATrack("3g0XVm6ZTWHbtTTfKhmMo7");