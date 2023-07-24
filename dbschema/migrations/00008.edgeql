CREATE MIGRATION m15rpqbbvxybyinnpbjmwemssvrwjtylc2goanu7nhl4qh3hpi634a
    ONTO m1ssdhklpp63xsctlxxu6rb2qbe3tnizrpyy2q33up3hs3or2nstkq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY salt {
          SET TYPE std::str USING (<std::str>'.salt');
      };
  };
};
