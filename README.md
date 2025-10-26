Leaderboard Popup

Prefabs
- LeaderboardPopupView – main leaderboard popup  
- PlayerInfoView – element to show one player's data  
- PlayerTypeStyles (ScriptableObject) – settings for color and scale by player type  
- ProjectContext – in Resources, contains Zenject bindings  

Scripts
- MainInstaller – binds dependencies.
- LeaderboardDataProvider – loads leaderboard data from "Resources/Leaderboard.json".
- LeaderboardPopupController – manages popup logic. 
- LeaderboardPopupMediator – opens and closes popup via PopupManagerService.  
- LeaderboardPopupModel – contains the list of players  
- LeaderboardPopupView – handles popup UI, initializing elements and buttons.
- PlayerInfoView – UI element for one player: name, score, avatar, type, color, scale.
- PlayerTypeStyleProvider – ScriptableObject for color and scale by player type.  
- PopupManagerService – service to manage popups via Addressables.
- LeaderboardButtonHandler – button to open the popup on the Menu scene.  

Workflow
1. Open popup
On the Menu scene, the button is linked to LeaderboardButtonHandler. When clicked, it calls CreatePopup() in LeaderboardPopupMediator.

2. Load popup
PopupManagerService loads LeaderboardPopupView via Addressables and calls Init() with LeaderboardPopupController as a parameter.
  
3. Prepare model
LeaderboardPopupController gets the player list from LeaderboardDataProvider (JSON in Resources) and creates LeaderboardPopupModel.

4. Initialize UI
For each player, a PlayerInfoView is created. Name, score, type, color, and scale are set via PlayerTypeStyleProvider. Avatar is loaded asynchronously with caching.
  
5. User interaction
Close button is linked to LeaderboardPopupController. On close, the element list is cleared, popup is disabled, and removed.
  
6. Avatar caching
Loaded avatars are stored in memory for reuse without extra requests.

7. Close popup
LeaderboardPopupMediator calls ClosePopup(), and PopupManagerService removes the popup instance.
