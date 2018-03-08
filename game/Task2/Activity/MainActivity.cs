using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using System;
using Android.Graphics;

namespace Task2
{
    [Activity(Label = "Task2", MainLauncher = true,Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        Button restartgame,button1, button2, button3, button4, button5, button6, button7, button8, button9, buttonreset,tempone,temptwo,tempthree;
        bool turnplayer1=true,win=false;
        int countplayer1=0,countplayer2=0,turn=0;
        TextView textViewplayer1point, textViewplayer2point;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            findallview();
            button1.Click += clickevent;
            button2.Click += clickevent;
            button3.Click += clickevent;
            button4.Click += clickevent;
            button5.Click += clickevent;
            button6.Click += clickevent;
            button7.Click += clickevent;
            button8.Click += clickevent;
            button9.Click += clickevent;
            buttonreset.Click += delegate
            {
                clearview();
                countplayer1= 0;
                countplayer2= 0;
                textViewplayer1point.Text = "player1:"+countplayer1 ;
                textViewplayer2point.Text = "player2:" + countplayer2 ;
            };
            restartgame.Click += delegate
            {
                clearview();
                textViewplayer1point.Text = "player1:" + countplayer1;
                textViewplayer2point.Text = "player2:" + countplayer2;
            };
        }

        private void clickevent(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if(!button.Text.Equals("-"))
            {
                return;
            }
            if(turnplayer1)
            {
                button.Text = "X";
                turnplayer1 = false;
                
            }
            else
            {
                button.Text = "O";
                turnplayer1 = true;
            }
            turn++;
            if (turn>4)
            {
                checkwin();
            }
            
        }

        /* private void checkwin()
         {//check for row
            if(button1.Text.Equals(button2.Text) && button1.Text.Equals(button3.Text) && !button1.Text.Equals("-"))
             {
                 if(button1.Text.Equals("X"))
                 {
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     win = true;
                     clearview();
                 }
                 if (button1.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             if (button4.Text.Equals(button5.Text) && button4.Text.Equals(button6.Text) && !button4.Text.Equals("-"))
             {
                 if (button4.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button4.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             if (button7.Text.Equals(button8.Text) && button7.Text.Equals(button9.Text) && !button7.Text.Equals("-"))
             {
                 if (button7.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button7.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             //check for coloumn
             if (button1.Text.Equals(button4.Text) && button1.Text.Equals(button7.Text) && !button1.Text.Equals("-"))
             {
                 if (button1.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button1.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             if (button2.Text.Equals(button5.Text) && button2.Text.Equals(button8.Text) && !button2.Text.Equals("-"))
             {
                 if (button2.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button2.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             if (button3.Text.Equals(button6.Text) && button3.Text.Equals(button9.Text) && !button3.Text.Equals("-"))
             {
                 if (button3.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button3.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             //check for diagonal
             if (button1.Text.Equals(button5.Text) && button1.Text.Equals(button9.Text) && !button1.Text.Equals("-"))
             {
                 if (button1.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button1.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             if (button3.Text.Equals(button5.Text) && button5.Text.Equals(button7.Text) && !button3.Text.Equals("-"))
             {
                 if (button3.Text.Equals("X"))
                 {
                     win = true;
                     countplayer1++;
                     Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                     clearview();
                 }
                 if (button3.Text.Equals("O"))
                 {
                     win = true;
                     countplayer2++;
                     Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                     clearview();
                 }

             }
             //for draw
             if (turn==9)
             {
                 Toast.MakeText(this, "Draw", ToastLength.Short).Show();
                 clearview();

             }



         }*/
        private void checkwin()
        {
            //for row
            if (comparetext(button1, button2, button3))
            {
                disableall(false);
                return;
            }
            if (comparetext(button4, button5, button6))
            {
                disableall(false);
                return;
            }
            if (comparetext(button7, button8, button9))
            {
                disableall(false);
                return;
            }
            //for column
            if (comparetext(button1, button4, button7))
            {
                disableall(false);
                return;
            }
            if (comparetext(button2, button5, button8))
            {
                disableall(false);
                return;
            }
            if (comparetext(button3, button6, button9))
            {
                disableall(false);
                return;
            }
            //for diagnol
            if (comparetext(button1, button5, button9))
            {
                disableall(false);
                return;
            }
            if (comparetext(button3, button5, button7))
            {
                disableall(false);
                return;
            }

            if (turn == 9)
            {
                Toast.MakeText(this, "Draw", ToastLength.Short).Show();
                clearview();

            }

        }

        private void disableall(Boolean b)
        {
            button1.Clickable = b;
            button2.Clickable = b;
            button3.Clickable = b;
            button4.Clickable = b;
            button5.Clickable = b;
            button6.Clickable = b;
            button7.Clickable = b;
            button8.Clickable = b;
            button9.Clickable = b;
        }

        bool comparetext(Button one,Button two,Button three)
        {
            if (one.Text.Equals(two.Text) && one.Text.Equals(three.Text) && !one.Text.Equals("-"))
            {
                if (one.Text.Equals("X"))
                {
                    win = true;
                    countplayer1++;
                    Toast.MakeText(this, "Player1 wins", ToastLength.Short).Show();
                    one.SetTextColor(Color.Red);
                    two.SetTextColor(Color.Red);
                    three.SetTextColor(Color.Red);
                    tempone = one;
                    temptwo = two;
                    tempthree = three;
                    return true;

                }
                if (one.Text.Equals("O"))
                {
                    win = true;
                    countplayer2++;
                    Toast.MakeText(this, "Player2 wins", ToastLength.Short).Show();
                    one.SetTextColor(Color.Red);
                    two.SetTextColor(Color.Red);
                    three.SetTextColor(Color.Red);
                    tempone = one;
                    temptwo = two;
                    tempthree = three;
                    return true;
                }
               
            }
            return false;
        }

        private void clearview()
        {
            textViewplayer1point.Text = "player1:" + countplayer1;
            textViewplayer2point.Text = "player2:" + countplayer2;
            tempthree.SetTextColor(Color.Black);
            temptwo.SetTextColor(Color.Black);
            tempone.SetTextColor(Color.Black);
            turn = 0;
            disableall(true);
            turnplayer1 = true;
            button1.Text = button2.Text = button3.Text = button4.Text = button5.Text = button6.Text = button7.Text = button8.Text = button9.Text = "-";
        }

        private void findallview()
        {
            restartgame = FindViewById<Button>(Resource.Id.restartgame);
            textViewplayer1point = FindViewById<TextView>(Resource.Id.textViewPlayer1);
            textViewplayer2point = FindViewById<TextView>(Resource.Id.textViewPlayer2);
            button1 = FindViewById<Button>(Resource.Id.button1);
            button2 = FindViewById<Button>(Resource.Id.button2);
            button3 = FindViewById<Button>(Resource.Id.button3);
            button4 = FindViewById<Button>(Resource.Id.button4);
            button5 = FindViewById<Button>(Resource.Id.button5);
            button6 = FindViewById<Button>(Resource.Id.button6);
            button7 = FindViewById<Button>(Resource.Id.button7);
            button8 = FindViewById<Button>(Resource.Id.button8);
            button9 = FindViewById<Button>(Resource.Id.button9);
            buttonreset = FindViewById<Button>(Resource.Id.buttonreset);
        }
    }
}

