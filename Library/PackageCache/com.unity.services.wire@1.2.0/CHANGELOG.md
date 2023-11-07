# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.2.0] - 2023-06-02
### Fixed
* fixed a bug in the ping protocol
* fixed an issue preventing the package to work with Nintendo Switch
* fixed vulnerabilities in the Websocket library
### Changed
* Excluded WebGL and Switch platforms from the websocket-sharp target

## [1.1.8] - 2023-05-31
### Fixed
* fixed a websocket bug referencing nonexistent Runtime object
* removed useless meta files from the package
### Changed
* internet reachability is no longer a hard requirement for package initialization

## [1.1.6] - 2023-04-17
### Fixed
* improved logging
* removed error log when unsubscribe arrives on an unsubscribed channel

## [1.1.5] - 2023-03-28
### Fixed
* Prevent the state changed event to fire twice on IChannel objects
* Fixed issue in the jslib adapter for WebGL build (experimental)

## [1.1.4] - 2023-03-20
### Changed
* Fix: unit tests are now synchronous
* Fix: threading simplification
* Fixed an issue that occured when a Subscription was subscribed and unsubscribed to repeatedly.

## [1.1.3] - 2023-03-08
### Changed
* Fix: Race condition at package initialization time

## [1.1.2] - 2023-02-15
### Changed
* Fix: Wire now tracks when a Authentication changes the current user Id and resets the connection accordingly.

## [1.1.1] - 2022-10-05
### Changed
* Fixed a situation where the network thread would access an object being owned by the main thread.
* Add a 10s retry time upon custom Wire close code

## [1.1.0] - 2022-08-04
### Changed
* Dependency version bump for core and auth package

## [1.0.1] - 2022-05-25
### Changed
  * The IChannel methods will now throw an ObjectDisposedException if the object was disposed.
  * Fix some cases of unexpected disconnection that didn't trigger a reconnect.

## [1.0.0-preview.14] - 2022-03-09
### Changed
  * IChannel will unsubscribe on Dispose but not if triggered by the finalizer.

## [1.0.0-preview.5] - 2022-02-26
  * Updated changelog.

## [1.0.0-preview.4] - 2022-02-25
  * Wire service URLs updated to reflect the new DNS domain.

## [1.0.0-preview] - 2022-02-21
### This is the first supported release of the *Wire* SDK.
  * fix package being stripped away from iOS build.
  * fix bug where Unity Editor would hang on domain reload.

## [0.0.1-preview] - 2021-09-29
### This is the first release of the *Wire* SDK.
- Working prototype of the Wire SDK package.
