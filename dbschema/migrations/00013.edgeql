CREATE MIGRATION m1khajwoozsyxehkii2py2rdd2xl4lbmjax4got2lcuxahccp4oq7q
    ONTO m1jibmoraxub4qf3mf7zgho3f4v6t4jcgna55kncos6f6kfhmetdvq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY username {
          CREATE CONSTRAINT std::exclusive;
      };
  };
};
