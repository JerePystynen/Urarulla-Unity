using System;

namespace DiMe.Urarulla
{
    /// <summary>
    /// This is the characteristics about the player, like how much of they are of something, ie. team player, solo doer, logical thinker, etc.
    /// </summary>
    [Serializable]
    public class Characteristics
    {
        public int creative;
        public int entrepreneuric;
        public int handy;
        public int leader;
        public int logical;
        public int physical;
        public int solo;
        public int team;
        
        public int greed;

        public Characteristics() { }

        public Characteristics(int creative, int entrepreneuric, int handy, int leader, int logical, int physical, int solo, int team, int greed)
        {
            this.creative = creative;
            this.entrepreneuric = entrepreneuric;
            this.handy = handy;
            this.leader = leader;
            this.logical = logical;
            this.physical = physical;
            this.solo = solo;
            this.team = team;
            this.greed = greed;
        }

        internal int[] GetScores()
        {
            return new int[] {
                creative,
                entrepreneuric,
                handy,
                leader,
                logical,
                physical,
                solo,
                team,
            };
        }
    }
}