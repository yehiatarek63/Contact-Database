CREATE MIGRATION m1jamgj6ucvmgcapa2wdrjncp5czfikmkkryaknutnjjzbplslj24a
    ONTO m1grbbz3elfkuq64hjinroigpmpa2x4hpy2q5dhu35l7zqgu77cvwq
{
  ALTER TYPE default::Contact {
      DROP PROPERTY salt;
  };
};
