# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2023-07-25
* Public GA release

## [1.1.0-pre.5] - 2023-06-14
* Added the new `InvalidJoinCode` error code
* Added the `ApiError` property to `LobbyServiceException`
* Updated exception messages to include full HTTP error details
* Removed error logs from HTTP exceptions
* Fixed missing updates to the internal lobby cache
* Updated Services Core to 1.8.2
* Updated Wire to 1.1.8

## [1.1.0-pre.4] - 2023-03-28

* Added more specific LobbyEvents
  * `PlayerJoined`: Raised when a player joins the lobby
  * `PlayerLeft`: Raised when a player leaves the lobby
  * `KickedFromLobby`: Raised when the owned player is kicked from a lobby
  * `PlayerDataChanged`: Raised when a player data changed
  * `PlayerDataRemoved`: Raised when a player data is removed
  * `PlayerDataAdded`: Raised when a player data is added
  * `DataChanged`: Raised when a lobby data changed
  * `DataRemoved`: Raised when a lobby data is removed
  * `DataAdded`: Raised when a lobby data is added
  * `LobbyDeleted`: Raised when a lobby is deleted
* Added password-protected lobbies
  * Added password to create lobby
  * Added password to join by id/code
  * Added password to update lobby (add, update, or remove password)
  * Added HasPassword field to the lobby model (automatically managed based on existence of password)
  * Added HasPassword as searchable field for queries

## [1.1.0-pre.3] - 2023-02-07

* LobbyEvents now provides a Version for comparison.
* Added CreateOrJoinLobby function to allow for a single call to create or join.
* Added PlayerProfile field to Player class

## [1.1.0-pre.2] - 2023-01-26

* Removed guards that were preventing Lobby Events APIs from being exposed completely.

## [1.1.0-pre.1] - 2022-09-13

* LobbyEvents functionality introduced to provide live change updates for lobbies.

## [1.0.3] - 2022-08-10

* Fixed a bug where ArgumentNullException would throw on LobbyConflict (HTTP 409) resolution.
* Fixed an issue where a CommonErrorCode was being reported instead of a LobbyExceptionReason, invalidating some switch cases.

## [1.0.2] - 2022-06-22

* Fixed some xml docs

## [1.0.1] - 2022-05-23

* Updating package dependency on com.unity.nuget.newtonsoft-json to 3.0.2

## [1.0.0] - 2022-05-13

### Changed

* Added Vivox / Lobby integration.
* Adding Reconnect API functionality.
* Stability improvements.

## [1.0.0-pre.7] - 2022-03-09

### Changed

* Introduced LobbyEvents functionality to allow receiving runtime updates when used with the Wire package.
* Replaced "Lobbies" and "ILobbyServiceSDK", replaced with "LobbyService" and "ILobbyService"
* Updating API Base Path.

## [1.0.0-pre.6] - 2021-10-14

### Changed

* Updated License.md.
* SDK is now generated with v0.7.0.
* Updated package dependency versions.
* Fixed testproject scene and gamemanager to use JoinCode to join Lobbies.

## [1.0.0-pre.5] - 2021-09-15

### Changed

* Update project file to include Lobby documentation link

## [1.0.0-pre.4] - 2021-08-19

### Changed

* Update to split test code into a separate .tests package.
* Updated Readme and Documentation.
* Updated package dependency versions.

## [1.0.0-pre.3] - 2021-08-18

### Changed

* Update to include user friendly wrapper for Open Beta.

## [1.0.0-pre.2] - 2021-08-06

### Changed

* Update `com.unity.services.core` dependency to latest `1.1.0-pre.4`

## [1.0.0-pre.1] - 2021-07-30

### Changed

* Re-version in preparation for Open Beta.

## [0.2.1-preview] - 2021-07-22

### Added

* Add Heartbeat API.

### Changed

* Update with latest HTTP code.

## [0.2.0-preview] - 2021-07-08

### Added

* Add preview tag.

## [0.2.0] - 2021-07-06

### This is the first release of *Unity Package Lobby*.

This package implements the client API for the Lobby service.
