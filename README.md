#wrappify
###### client wrapper to connect Spotify web api

create new instance


    IRequestOperation requestOperation = new RequestOperation();
    ISpotifyClient spotifyClient = new ISpotifyClient(requestOperation);


if you need to access personal data, you have to set and access token

    spotifyClient.SetAccessToken("access_token");

call method

    User user = await spotifyClient.GetCurrentUserProfileTest();
    
    User user = await spotifyClient.GetUser("username here");
    
    Paging<Playlist> playlistPaging = await spotifyClient.GetAUsersPlaylists("username here");
    
    Track track = await spotifyClient.GetATrack("3g0XVm6ZTWHbtTTfKhmMo7");
	
	
some of methods has not been added yet. as soon as possible i will add them