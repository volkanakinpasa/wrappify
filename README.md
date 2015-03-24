#wrappify
###### client wrapper to connect Spotify web api

[![Build status](https://ci.appveyor.com/api/projects/status/is9kqbv2ous3aypd/branch/master?svg=true)](https://ci.appveyor.com/project/volkanakinpasa/wrappify/branch/master)

####**create new instance**


      RequestConfiguration requestConfiguration = new RequestConfiguration(Host, Port, Scheme);
      //or initialize 'HttWrap' Wrapper library
      //IHttpWrapper httpWrapper = new HttWrapWrapper(requestConfiguration);
      IHttpWrapper httpWrapper = new HttpWrapper(requestConfiguration);
      
      ISpotifyClient spotifyClient = new SpotifyClient(httpWrapper);




####**if you need to access personal data, you have to set and access token**

    spotifyClient.SetAccessToken("access_token");

####**call methods**

    User user = await spotifyClient.GetCurrentUserProfileTest();
    
    User user = await spotifyClient.GetUser("username here");
    
    Paging<Playlist> playlistPaging = await spotifyClient.GetAUsersPlaylists("username here");
    
    Track track = await spotifyClient.GetATrack("3g0XVm6ZTWHbtTTfKhmMo7");
	
	
####**Authorization Code Flow**

![enter image description here](https://developer.spotify.com/wp-content/uploads/2014/04/Authorization-Code-Flow-Diagram.png)
 

**PS**: You need to know [Authorization Code Flow click here for more info ](https://developer.spotify.com/web-api/authorization-guide/)