#### 2.0.0 (2021-04-09)

- Lots of breaking changes.
- Lots of improvements.
- Lots of normalizations.
- Has complete XML documentation.
- Properly asynchronous.

#### 1.0.8 (2021-04-02)

- Changed `VehicleResponse.FuelRemaining` to a `byte` data type since its value range is only 0-100.

#### 1.0.7 (2020-05-27)

- Internal code clean up.
- Improved `debug` flag handling.

#### 1.0.6 (2020-05-07)

- Targeting .NET Standard 2.0 now.
- Internal code clean up and rearrangement. Hopefully some performance optimizations by adding `ConfigureAwait(false)` to all `await` calls.