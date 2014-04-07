using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter
{
    [Serializable]
    class HighScoresList
    {
        public List<HighScoresEntry> highScoresList = new List<HighScoresEntry>();

        public HighScoresList()
        {

        }
    }
}
