using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shooter
{
    [Serializable]
    public class HighScores
    {
        int score;
        
        string name;

        HighScoresList scores;

        [NonSerialized]
        FileStream s; //to deserialize from in Read()

        [NonSerialized]
        IFormatter file = new BinaryFormatter();

        public HighScores(int score)
        {
            this.score = score;

            s = new FileStream("ShooterHiScores.txt", FileMode.Open);
        }


        public void Read()
        {
            string list = "";

            s.Position = 0;
            scores = (HighScoresList)file.Deserialize(s);

            var sortedList = scores.highScoresList.OrderByDescending(entry => entry.score);

            foreach (HighScoresEntry entry in sortedList)
            {
                list += entry.score + "              " + entry.name + "\r\n";
            }

            Form form = new Form();
            TextBox txtBox = new TextBox();
            txtBox.Text = list;
            txtBox.Multiline = true;
            txtBox.Width = 300;
            txtBox.Height = 300;
            txtBox.ScrollBars = ScrollBars.Vertical;
            form.Controls.Add(txtBox);
            form.Show();

            s.Position = 0; //everytime you de/serialize, the position moves, so reset it back to beginning

            file.Serialize(s, scores);
        }

        public void Write()
        {

            s.Position = 0;

            name = Interaction.InputBox("Enter Your Name", "High Scores List Entry", "Anonymous");
            
            if (s.Length == 0)
            {
                scores = new HighScoresList();
            }
            else
            {
                scores = (HighScoresList)file.Deserialize(s);
            }

            scores.highScoresList.Add(new HighScoresEntry(score, name));

            s.Position = 0;

            file.Serialize(s, scores);
            

            
        }

        public void CloseStream()
        {
            s.Close();
        }
    }
}
