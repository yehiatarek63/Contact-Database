CREATE MIGRATION m1grbbz3elfkuq64hjinroigpmpa2x4hpy2q5dhu35l7zqgu77cvwq
    ONTO m13g7wb4uzsyrm22actsbozn7ynu3q4apyc57zb2mc7ptlk6ccvdla
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY salt {
          RESET OPTIONALITY;
      };
  };
};
