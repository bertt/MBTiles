# MBTiles

.NET 6 Library for reading/writing MBTiles files

## NuGet

https://www.nuget.org/packages/bertt.MBTiles/

## Sample code:

```
var conn = MBTilesWriter.CreateDatabase(db, metadata);
MBTilesWriter.WriteTile(conn, t, content1);
```

## Dependencies

- System.Data.Sqlite https://www.nuget.org/packages/System.Data.SQLite/

- Tilebelt  https://www.nuget.org/packages/tilebelt/

## History

2022-10-30: release 2.0, 2.0.1, 2.0.2, to .NET 6

2020-04-18: release 1.0