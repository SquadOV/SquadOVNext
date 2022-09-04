# Motivation

SquadOV is built to provide gamers the best tool to record, review, and share their gaming VODs and clips.
The original service released in 2021 was cloud-first application.
While this made the "sharing" aspect easy, unfortunately the service was too costly to continue to maintain and run (storing videos is expensive, who knew).
Thus, leading to the creation of SquadOV NEXT (hereby referred to as SquadOV) - an open source, community driven app that has minimal server-side components.
Of course, the downside of this is that sharing VODs and clips becomes a much less straight-forward process; however, this should be work-aroundable.

# Programming Languages

For the user interface, C# will be used due to the choice of using Avalonia.
Functionality that the UI uses can be encapsulated within libraries - any programming language that creates a library that can be DllImport-ed into C# can be used.
For official development and plugins, C++, Rust, and C# are to be preferred.

# Licensing Choice

GPLv3 was chosen primarily because of the desire to use libx264 in FFmpeg. That's it. :)

# Desired Functionality

SquadOV is primarily meant to be used in the context of video games (though many of its components could theoretically be used in other areas).
The program should be made up of a number of components that combine together into a single program to support the following features:

* Recording
* Data Collection
* Organization and Search
* Analysis
* Editing
* Annotation
* Sharing (desktop, web, mobile)

Furthermore, this program should be able to run cross-platform.
Thus, after consideration of the libraries needed to implement these features, the [Avalonia](https://avaloniaui.net/) UI framework was chosen.

## Recording

Recording is the process of either recording individual game VODs or full-length session VODs.
There are three main components to this:

* Image Capture and Processing
* Audio Capture and Processing
* Video Encoding

### Image Capture and Processing

Image capture is dependent on native APIs.
On Windows, this will either be via DirectX hooks or via [DXGI desktop duplication](https://docs.microsoft.com/en-us/windows/win32/direct3ddxgi/desktop-dup-api).
Image processing will also be dependent on native APIs (e.g. on Windows via DirectX).
Users should have the option of adding overlays to their captured images (i.e. to hide game chat).

### Audio Capture and Processing

Audio capture is dependent on native APIs.
On Windows, this will be done using [WASAPI](https://docs.microsoft.com/en-us/windows/win32/coreaudio/wasapi).
Audio processing can be done using a DSP library such as [KFR](https://www.kfrlib.com/).
Alternatively, some filters that are built into FFmpeg can also be used.
Furthermore, it's important to support recording audio into separate audio tracks as well.

### Video Encoding

Video encoding can be done using [FFmpeg](https://ffmpeg.org/).
SquadOV should support the most popular codecs and container formats.

Container Formats:
* MP4
* WebM
* HLS

Video Codecs:
* H.264
* H.265
* VP9
* AV1

Audio Codecs:
* AAC
* Opus

Video encoding should output to the local filesystem directly and should use GPU-acceleration when possible.

## Data Collection

In addition to recording video, SquadOV must also collect game data to be combined with videos (1 video can have multiple games) for organization, search, and analysis.
We will view game data in three lenses:

* Raw
* Processed
* Reports

Raw data is what SquadOV collects from the game (via API calls, packet sniffing, memory reading, log reading, replay files, etc.).
Processed data is what SquadOV will convert the raw data into for further processing (e.g. turning raw log lines into objects more easily used by machines).
Reports are what SquadOV will use to display information about the game - the contexts around the reports should be standardized (e.g. match summary, player summary, events, etc.) though the exact way they're rendered should be game-specific.
All three forms of data will be stored on the local filesystem in a compressed format - in particular [JSON](https://www.json.org/json-en.html) and [Avro](https://avro.apache.org/) are well-suited for this task. 

## Organization and Search

Everything will be filesystem based (i.e. nothing gets stored in the cloud) for simplicity.
The data will be stored in 3 components:

* Search Database
* VOD Database
* Match Database

The search database will use [Lucene.NET](https://lucenenet.apache.org/) to store documents that reference the VOD and the matches within the VOD (along with the important searchable metadata that should be generated as a report).

The VOD and match databases will primarily be folders on the user's filesystem.
Note that it should be possible to "add" new VOD/match databases (e.g. if we wanted to add data another user collected).
These databases will also contain more traditional "database" files to store metadata about the video/match (e.g. start/stop timestamp).
[RocksDB](http://rocksdb.org/) will be used as a key-value store to store this information.
For VODs, the key will be the [SHA256](https://en.wikipedia.org/wiki/SHA-2) hash of the video (thus allowing users to rename video files if they want).
For matches, the key will be the [UUID](https://en.wikipedia.org/wiki/Universally_unique_identifier) we generate for each match.

## Analysis

The primary way we want to support analysis is via events and general stats (within a single game as well as as across multiple games).
Events are the primary driver of all analysis where by certain timestamps (or sections of the match) are marked as being important and these moments in time in the video can be easily navigated to.
The specific events generated for a match is dependent on the game being watched.
This is also true for how the event should be displayed (the only commanality being a way to go to that point in the video) and the available ways to filter events.
Furthermore, users should be given a way to easily extend what events are displayed for each game by writing their own custom processing functions that takes in processed data for the game and spits out a custom event report.

SquadOV should also contain a generic view of "stats."
These stats should have some value for each match played (e.g. # of kills, HS%, etc.) for all players involved.
For the local player, we should also store these stats across multiple games and provide easy ways to show aggregate values (e.g. the player's latest rank, HS% across a 30-day period averaged over 3 day periods, etc.).
The stats to display will be dependent on the game.
For storage for individual matches, these stats should be generated as a report.
For storage across mutiple matches for the local player, stats can be further cached in [Couchbase Lite](https://www.couchbase.com/products/lite) for easy querying.

## Editing

The editing tools should be similar to what one gets from traditional video editing software like Adobe Premiere Pro and DaVinci Resolve.
However, the primary functionality that most users will use will be:

* Clipping
* Drawing/adding overlays (images, GIFs, etc.)
* Changing the volume/muting audio
* Adding audio snippets/music

Additional advanced functionality can be considered as well down the road.

## Annotation

SquadOV must also provide users a way to add notes to their VODs.
These notes can be in the form of text comments, audio snippets, or even video snippets.
Optionally, these notes can be tied to a certain point (or range) in time in the VOD.
Furthermore, users should be allow to tie drawings on the video to certain comments to allow users to highlight certain parts of the screen with respect to the comment.
These notes should be stored in a [Couchbase Lite](https://www.couchbase.com/products/lite) database for easy insertion/deletion/querying.

## Sharing

Sharing will be the most complex aspect of this since all data will be stored on a user's local machine.
Furthermore, we want to be able to selectively share VODs to certain people depending on certain properties in the match metadata.
We therefore have two options:

1. Implement an offline-first, P2P solution
2. Rely on external programs (e.g. Dropbox) to handle sync for us

Unfortunately, using external programs won't allow us to perform selective sharing so we need to implement a P2P solution.

### Peer Discovery

We can draw inspiration from programs like [Retroshare](https://retroshare.cc/).
Retroshare uses a [DHT](https://en.wikipedia.org/wiki/Distributed_hash_table) to discover peers.
Users have to share a "certificate" file with eachother where the peer would use that certificate to lookup the value in the DHT to get the proper network address to communicate with.
Then, a direct UDP connection is established between every pair of peers and getting through NAT via UPnP or NAT-PMP.
It'd be fairly simple to parallel this structure using [OpenDHT](https://github.com/savoirfairelinux/opendht).
We can also establish "squads" as paralleling BitTorrent swarms and have users connect to peers in a group fashion.

### External Sharing

External sharing (e.g. via a web browser link) won't be possible without server infrastructure.
Users will need to upload the VOD as well as the relevant match data to either our servers or some other server before we can display that information in a web browser.
Theoretically, we could have the user upload to a service such as Dropbox and just have the web app pull from the correct link.
TBD.

# Plugin Architecture

SquadOV should be designed with clean interfaces that allow it to be easily extended with additional functionality.
A couple of things that this system should support are:

* Supporting new games
* Adding new image overlay/processing components/filters
* Adding new audio processing components/filters
* Custom report generation extensions
