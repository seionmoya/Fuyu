# TODO

## Infrastructure

### Modding

- [x] Write generic modloader
- [x] implement dll modding through generic modloader
- [x] implement script modding through generic modloader
- [x] Migrate from BepInEx to NLog hook
- [x] Support client dll loading
- [ ] Support client script loading
- [ ] Support launcher dll loading
- [ ] Support launcher script loading
- [x] Support backend dll loading
- [x] Support backend script loading

### Dependency injection

- [ ] Switch from static class to DI singletons

### Networking

- [x] Http server
- [x] WS server

### Types

- [x] MongoId type
- [x] MongoId serializer
- [x] Union type
- [ ] Union serializer (TODO: stop throwing exceptions)
- [x] Custom threaded collections

### Utilities

- [x] JSON parsing/serializing
- [x] VFS - File system wrapper
- [ ] AES encryption/decryption
- [x] Resx - Embedded dll resource loading

## Launcher

- [x] Create account
- [x] Login to account
- [x] Add game to account
- [x] Select game edition to add to account
- [x] Start game
- [ ] Persistent settings
- [ ] Show all games available in cards
- [ ] Show all games in your library in cards
- [ ] Game overview pages
- [ ] Show game-related stats on game page

## EFT

### Infrastructure

- [ ] ETag support (TODO: broke in 0.16.0!)

### Assembly tooling

- [x] DLL hollower
- [ ] Deobfuscate DLL (needed for FIKA support)
- [ ] Assembly remapping (needed for FIKA support)

### Client

- [X] Disable BattlEye patch
- [x] Disable Consistency on Assembly-CSharp.dll patch
- [x] Comminucate over http patch

### Item Events

- [x] Add note
- [x] Edit note
- [x] Remove note
- [x] Apply inventory changes (when leaving stash menu)
- [x] Fold item
- [x] Bind item to fastpanel
- [ ] Unbind item from fastpanel
- [x] Wishlist
- [x] Examine item
- [ ] Sync items between games (EFT <-> EFT Arena)
- [ ] Read Encyclopedia
- [x] Recode item
- [ ] Use medkit
- [ ] Sell all items on player scav
- [ ] Buy customization (from trader)
- [ ] Insure item
- [ ] Repair item
- [x] Move item
- [x] Tag item
- [ ] Toggle item
- [ ] Item size calculation (broken!)
- [ ] Create item
- [ ] Split item

### Lobby

- [ ] Functional websocket retention (lookup based sessid and uuid)
- [ ] Reply Ping packet
- [ ] LobbyService for sending messages through websocket

### Raid

- [ ] Create a new match when enterning "looking for group" if no match exists on location+time
- [ ] Generate loot for match
- [ ] Allow player to join group in match
- [ ] Save raid settings on the backend
- [ ] Save player progression after raid
- [ ] Allow healing through raid end screen

### Insurance

- [ ] Add insurance to item (ItemEvent)
- [ ] Lose insured item when missing in profile save
- [ ] Trader send back item over mail

### Friends

- [ ] Add friend
- [ ] Remove friend
- [ ] Block friend
- [ ] Mute friend
- [ ] Find profile
- [ ] View profile

### Dialogue

- [ ] Receive messages
- [ ] Send message
- [ ] Pin message
- [ ] Make group leader
- [ ] Switch group leader
- [ ] Remove group leader
- [ ] Receive attached items

### Raid group

- [ ] Create raid group
- [ ] Remove raid group
- [ ] Leave raid group
- [ ] Transfer group leader
- [ ] Group ready
- [ ] Group not ready

### Survey

- [ ] Show survey
- [ ] Submit survey

### Hideout

- [ ] Upgrade hideout area
- [ ] Complete hideout area construction
- [ ] Apply hideout area bonusses
- [ ] Buy hideout customization
- [ ] Apply hideout customization
- [ ] Produce item
- [ ] Hideout specific area functions

### Quests

Notes:

- According to DeadLeaves, one of the most poorly structured data from BSG.

Implementation idea is as follows:

- Register to a profile _ONLY_ quests that should be listed.
- Server -> Client synchronization happens over ItemEvent
- Client -> Server synchronization happens over WebSocket

#### Trader quests

- [ ] Show available quests
- [ ] Start quest
- [ ] Handle quest condition counters
- [ ] Submit items for quest
- [ ] Complete quest
- [ ] Fail quest
- [ ] Redo failed quest
- [ ] Unlock next quest in quest chain
- [ ] Unlock trader quest offers
- [ ] Send messages

#### Daily quests

- [ ] Handle time-sensitive quests
- [ ] Handle EFT <-> Arena objectives

### BSG Trading

Notes:

- Ragfair is game's internal name for Flea Market

Implementation idea is as follows:

- Trader offer template act as source for regenerating trader offers
- Ragfair offers holds all available offers
- Ragfair handles all buy/sell actions
- Available trader offers should be obtained from Ragfair database (trader assorts and prices requests generates from ragfair data)
- Trader buy/sell should be piped to ragfair

#### Infrastructure

- [ ] Add TradeDatabase to `Fuyu.Backend.BSG` (stores ragfair offers, trader termplate, trader assortiment template, cloth assortiment template)
- [ ] Add TradeOrm to `Fuyu.Backend.BSG`
- [ ] Add `Servers/TradeServer` to `Fuyu.Backend.BSG` (port 8030)

#### Trading

(I know nexus is onto this, no clue about the current status)

- [ ] Show traders
- [ ] Restock traders
- [ ] Buy cash offer
- [ ] Buy barter offer
- [ ] Buy preset item
- [ ] Sell items
- [ ] Sell presets
- [ ] Ref trader sync with EFT Arena

##### Ragfair

(I know nexus is onto this, no clue about the current status)

- [ ] Show offers
- [ ] Show offers in pages
- [ ] Filtering offers
- [ ] Purchase offer
- [ ] Linked search
- [ ] Required search
- [ ] Current search
- [ ] Add player offer
- [ ] Remove player offer
