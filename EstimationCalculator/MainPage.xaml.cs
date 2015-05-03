using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EstimationCalculator.Resources;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using System.Collections;

namespace EstimationCalculator
{
    public static class GlobalVariables
    {
        public static int TotalNumberOfRequestedCalls = 0;

        public static int RiskFactor = 0;

        public static int BidCalls = 0;

        public static int OrderOfRound = 1;

        public static bool GameOver = false;

        public static bool GameUnder = false;

        public static int ALOPSC = 1;

        public static int tempALOPSC = 1;

        public static bool CheckAllLosers = false;

        public static int LoosersCounter = 0;

        public static int iSheet = 0;

        public static bool CreateSheet = false;
        //the next variable used for the scoresheet
        public static int[] RiskFactors = new int[18];
        public static int[] ALOPSCs = new int[18];

        public static Stack<int> UndoRedoStack1 = new Stack<int>();
        public static Stack<int> UndoRedoStack2 = new Stack<int>();
        public static Stack<int> UndoRedoStack3 = new Stack<int>();
        public static Stack<int> UndoRedoStack4 = new Stack<int>();

        public static bool CanRedo = false;

        public static int BDCMadeAt = 0;
    }

    public class GlobalTable
    {
        public string name = null;
        public bool bid = false;
        public bool DashCall = false;
        public bool DashBlind = false;
        public bool DashBlind_Chances = true;
        public int NumberOfRequestedCalls = 0;
        public int NumberOfMadeCalls = 0;
        public bool with = false;
        public bool risk = false;
        public bool win = false;
        public bool only_winner = false;
        public bool only_loser = false;
        public int GameScore = 0;
        public int TotalScore = 0;
        //the next 8 variables used for the score sheet
        public int[] AllScores = new int[18];
        public bool[] bids = new bool[18];
        public char[] BidSuits = new char[18];
        public bool[] DashCalls = new bool[18];
        public bool[] DashBlinds = new bool[18];
        public bool[] Withs = new bool[18];
        public bool[] Risks = new bool[18];
        public bool[] OnlyWinners = new bool[18];
        public bool[] OnlyLosers = new bool[18];
        //
        
        public int TempScore = 0;
        public bool TempBids;
        public char TempBidSuits;
        public bool TempDashCalls;
        public bool TempDashBlinds;
        public bool TempWiths;
        public bool TempRisks;
        public bool TempOnlyWinners;
        public bool TempOnlyLosers;
        public bool BDCUndoed = false;
      
    }

    public static class Table
    {
        public static GlobalTable[] Player = new GlobalTable[4];
        static Table()
        {
            for (int i = 0; i < 4; i++)
            {
                Player[i] = new GlobalTable();

                for (int j = 0 ; j < 18 ; j++)
                {
                    Player[i].OnlyLosers[j] = false;
                    Player[i].OnlyWinners[j] = false;
                    Player[i].Withs[j] = false;
                    Player[i].Risks[j] = false;
                    Player[i].DashBlinds[j] = false;
                    Player[i].DashCalls[j] = false;
                }
            }
        }
    }
       
    public partial class MainPage : PhoneApplicationPage
    {       
        DispatcherTimer MyTimer = new DispatcherTimer();

        public int counter = 0;
 
        public MainPage()
        {
            InitializeComponent();

            MyTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();          
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            counter++;
            if ( counter == 3 )
            {
                MyTimer.Stop();
                NavigationService.Navigate(new Uri("/StartPage.xaml", UriKind.Relative));
            }                
        }
    }
}