# TODO

## Infrastructure

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

## EFT

### Infrastructure

- [ ] ETag support (TODO: broke in 0.16.0!)

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

#### Daily quests

- [ ] Handle time-sensitive quests
- [ ] Handle EFT <-> Arena objectives

### BSG Trading

Notes:

- Ragfair is game's internal name for Flea Market

Implementation idea is as follows:

- Trader offer database act as source for regenerating trader offers
- Ragfair database holds all available offers
- Ragfair handles all buy/sell actions
- Available trader offers should be obtained from Ragfair database
- Trader buy/sell should be piped to ragfair

### Trading

(I know nexus is onto this, no clue about the current status)

- [ ] Show traders
- [ ] Buy cash offer
- [ ] Buy barter offer
- [ ] Buy preset item
- [ ] Sell items
- [ ] Sell presets

#### Ragfair

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
