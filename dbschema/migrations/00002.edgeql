CREATE MIGRATION m1bfevbux76455sj35bg5n34pvyivluqgnwmlnmlyh5jmgb6jhv2ya
    ONTO m1w67s2cuafighy72qu7vjjkixy6zklknbvh3pfs6kkvmumrs5uokq
{
  CREATE SCALAR TYPE default::State EXTENDING enum<Mr, Mrs, Ms, Dr, Prof>;
  ALTER TYPE default::Contact {
      ALTER PROPERTY title {
          SET TYPE default::State;
      };
  };
};
