# MBTiles

.NET Standard Library for reading/writing MBTiles files

NuGet: https://www.nuget.org/packages/bertt.MBTiles/

Sample code:

```
var conn = MBTilesWriter.CreateDatabase(db, metadata);
MBTilesWriter.WriteTile(conn, t, content1);
```
