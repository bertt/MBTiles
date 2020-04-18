BEGIN;

CREATE TABLE IF NOT EXISTS tiles (
   zoom_level integer,
   tile_column integer,
   tile_row integer,
   tile_data blob
);

CREATE TABLE IF NOT EXISTS metadata (
    name text,
    value text
);

CREATE UNIQUE INDEX IF NOT EXISTS tile_index ON tiles (zoom_level, tile_column, tile_row);
CREATE UNIQUE INDEX IF NOT EXISTS name ON metadata (name);

COMMIT;
