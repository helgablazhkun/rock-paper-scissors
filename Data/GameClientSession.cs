namespace rock_paper_scissors.Data
{
    public class GameClientSession
    {
        public string Nick { get; set; }
        public string WeaponRound1 { get; set; }
        public string WeaponRound2  { get; set; }
        public string WeaponRound3  { get; set; }
        public string WeaponRound4  { get; set; }
        public string WeaponRound5  { get; set; }
    }

    public class FinalGameStatistic {
        public string PlayerNick  { get; set; }
        public string OpponentNick  { get; set; }
        public string PlayerFinalResult {get;set;}
        public FinalRoundResult Round1Result  { get; set; }
        public FinalRoundResult Round2Result  { get; set; }
        public FinalRoundResult Round3Result  { get; set; }
        public FinalRoundResult Round4Result  { get; set; }
        public FinalRoundResult Round5Result  { get; set; }
    }

    public class FinalRoundResult {
        public string PlayerWeapon {get; set;}

        public string OpponentWeapon {get; set;}

        public string PlayerResult {get;set;}

    }

}