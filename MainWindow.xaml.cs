using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace standfordprisiongame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
     
            // Instantiate the player class we created earlier
        private Player _player = new Player();
        private Random _rng = new Random();

        private int _currentScenarioIndex = 0;
        private List<Scenario> _timeline = new List<Scenario>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimeline(); // Populates your scenario cards

            // RUN THE COIN FLIP IMMEDIATELY AT BOOT UP
            DetermineRandomRole();
        }

        private void DetermineRandomRole()
        {
            int coinFlip = _rng.Next(0, 2);

            if (coinFlip == 0)
            {
                _player.AssignedRole = Role.Prisoner;
                _player.ID = "Prisoner #" + _rng.Next(100, 999);
                _player.Compliance = 50;

                // Dynamically write the title screen text
                if (DisclaimerText != null)
                {
                    DisclaimerText.Text = $"You have been randomly assigned the role of STATUS: {_player.ID}. Your objective is to maintain mental stability while navigating the oppressive social dynamics of the facility. Exit is only possible through authorized release or containment breach.";
                }
            }
            else
            {
                _player.AssignedRole = Role.Guard;
                _player.ID = "Guard (Shift A)";
                _player.Compliance = 100;

                if (DisclaimerText != null)
                {
                    DisclaimerText.Text = "You have been randomly assigned the role of GUARD. Your objective is to maintain absolute authority and control over the cell block. You must follow the structural orders of the facility while monitoring your own psychological stress levels.";
                }
            }
        }

        private void InitializeTimeline()
        {
            _timeline.Add(new Scenario(
                "DAY 1: ID Count. Guards wake everyone up at 2:00 AM, blowing whistles loudly right by the cell bars.",
                "Stand perfectly straight and yell out your number.", "Refuse to stand up and mumble from your cot.",
                "Force them to do push-ups if they mix up their numbers.", "Let them slide by with quiet warnings.",
                new double[] { 5, 2 }, new double[] { 12, 8 }
            ));

            _timeline.Add(new Scenario(
                "DAY 2: Cleanliness. Guards order prisoners to clean the toilet bowls with their bare hands as a compliance test.",
                "Scrub the basin silently without making eye contact.", "Throw the cleaning rag at the guard's feet.",
                "Threaten them with solitary confinement if they slow down.", "Hand them gloves to make the chore more humane.",
                new double[] { 10, 15 }, new double[] { 20, 10 }
            ));

            _timeline.Add(new Scenario(
                "DAY 3: The Hunger Strike. Prisoner #819 refuses to eat his sausage rations to protest the living conditions.",
                "Eat your own food quietly to avoid getting caught up in it.", "Join the strike and push your plate away.",
                "Lock the striking prisoner in the dark closet storage room.", "Sit down and try to convince him to take a few bites.",
                new double[] { 8, 4 }, new double[] { 15, 12 }
            ));

            _timeline.Add(new Scenario(
                "DAY 4: Blanket Confiscation. As punishment for a minor infraction, guards decide to take away all pillows and blankets.",
                "Sleep directly on the cold concrete floor without complaining.", "Organize a group chant demanding your bedding back.",
                "Keep the cell block lights on all night to disrupt their sleep.", "Quietly look away and let them keep one blanket.",
                new double[] { 12, 10 }, new double[] { 18, 5 }
            ));

            _timeline.Add(new Scenario(
                "DAY 5: Visiting Hours. Parents are arriving to inspect the facility. The supervisor demands everything look pristine.",
                "Wash your uniform and tell your parents everything is fine.", "Whisper the truth to your mother when guards step away.",
                "Shave the prisoners' heads and force them to smile for guests.", "Allow them an extra 10 minutes of private family time.",
                new double[] { 6, 8 }, new double[] { 22, 14 }
            ));
        }

        private void LogEvent(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm");
            LogText.Text += $"\n[{timestamp}] {message}";

            // Auto-scroll logic: ensures user doesn't have to manually scroll down
            LogScroller.UpdateLayout();
            LogScroller.ScrollToEnd();
        }

        private void EvaluateExperimentEnding()
        {
            LogEvent("\n=== EXPERIMENT TERMINATED ===");

            if (_player.AssignedRole == Role.Prisoner)
            {
                if (_player.MentalStability <= 0)
                    LogEvent("CRITICAL BREAKDOWN: Your subject suffered severe emotional trauma. The lead researcher steps in to release you early.");
                else if (_player.Compliance <= 0)
                    LogEvent("TOTAL REBELLION: Your compliance hit 0%. You have completely broken the authority structure of the prison.");
                else if (_player.Compliance >= 100)
                    LogEvent("SYSTEMIC ASSIMILATION: Your compliance hit 100%. You have completely shed your original identity, becoming an entirely obedient number.");
            }
            else // Guard Endings
            {
                if (_player.MentalStability <= 0)
                    LogEvent("PSYCHOLOGICAL COLLAPSE: The guilt and stress of enforcing tyrannical orders caused you a severe moral breakdown. You drop your nightstick and walk out.");
                else if (_player.Compliance <= 0)
                    LogEvent("MUTINY: Your authority alignment hit 0%. The prisoners no longer respect your commands, and management fires you for being too soft.");
                else if (_player.Compliance >= 100)
                    LogEvent("TOTAL DEINDIVIDUATION: Your alignment hit 100%. You have completely absorbed the brutal 'Guard' persona, becoming entirely desensitized to human suffering.");
            }

            DisableButtons();
        }

        private void UpdateUI()
        {
            // 1. Keep your Dynamic Header running so names and roles update
            if (StatusHeader != null)
            {
                StatusHeader.Text = $"SUBJECT STATUS: {_player.ID} | ROLE: {_player.AssignedRole}";
            }

            // 2. Keep the progress bars perfectly in sync with C# values
            StabilityBar.Value = _player.MentalStability;
            ComplianceBar.Value = _player.Compliance;

            // 3. Visual Feedback: If stability drops too low, turn the bar red
            if (_player.MentalStability < 30)
            {
                StabilityBar.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                // Resets it back to your original teal color if stability goes back up
                StabilityBar.Foreground = System.Windows.Media.Brushes.DarkCyan;
            }

            // NOTE: The old 'HasBrokenDown' check has been safely removed from here!
        }



        private void OnObeyClick(object sender, RoutedEventArgs e)
        {
            if (_currentScenarioIndex >= _timeline.Count) return;

            string currentChoice = BtnObey.Content.ToString();
            LogEvent($"[DECISION] You chose to: {currentChoice}");

            if (_player.AssignedRole == Role.Prisoner)
            {
                // Prisoner path: Obeying increases compliance but erodes identity
                _player.Compliance += 15;
                _player.ProcessPsychologicalImpact(stressIncr: 5, stabilityDecr: 5);
            }
            else
            {
                // Guard path: Obeying means enforcing harsh psychological rules
                // This increases their systemic authority alignment, but causes moral trauma
                _player.Compliance += 15; // Here, compliance acts as your "Authoritarian Alignment" meter
                _player.ProcessPsychologicalImpact(stressIncr: 15, stabilityDecr: 12); // High mental stability cost!

                LogEvent("Systemic pressure forces you to act harshly. Your authority is secure, but your conscience suffers.");
            }

            _player.Compliance = Math.Clamp(_player.Compliance, 0, 100);

            UpdateUI();
            DisplayCurrentScenario();
        }

        private void OnRebelClick(object sender, RoutedEventArgs e)
        {
            if (_currentScenarioIndex >= _timeline.Count) return;

            string currentChoice = BtnRebel.Content.ToString();
            LogEvent($"[DECISION] You chose to: {currentChoice}");

            if (_player.AssignedRole == Role.Prisoner)
            {
                // Prisoner path: Resisting drops compliance and incurs guard punishment
                _player.Compliance -= 15;
                _player.ProcessPsychologicalImpact(stressIncr: 15, stabilityDecr: 10);
            }
            else
            {
                // Guard path: Showing leniency or refusing psychological cruelty
                // This preserves your mental stability, but you lose authority control over the prison
                _player.Compliance -= 20;
                _player.ProcessPsychologicalImpact(stressIncr: 5, stabilityDecr: -5); // Notice the negative value: this RESTORES 5 stability!

                LogEvent("You chose human decency over prison rules. You feel more stable, but you are losing control of the cell block.");
            }

            _player.Compliance = Math.Clamp(_player.Compliance, 0, 100);

            UpdateUI();
            DisplayCurrentScenario();
        }

        private void OnEscapeClick(object sender, RoutedEventArgs e)
        {
            if (_player.AttemptEscape())
            {
                LogEvent("SUCCESS: You found a lapse in security and fled the basement.");
                UpdateUI();
                // Logic for winning could go here
            }
            else
            {
                LogEvent("FAILURE: The guards caught you at the back door. Punishment is severe.");
                UpdateUI();
            }
        }

        private void OnStartGameClick(object sender, RoutedEventArgs e)
        {
            // FORCE RESET: Turn the buttons back on for the new game!
            // Paste this at the top of OnStartGameClick in MainWindow.xaml.cs
            BtnObey.IsEnabled = true;
            BtnRebel.IsEnabled = true;
            BtnEscape.IsEnabled = true;

            // Force the backgrounds visually via C#
            BtnObey.Background = System.Windows.Media.Brushes.DimGray;
            BtnObey.Foreground = System.Windows.Media.Brushes.White;
            BtnRebel.Background = System.Windows.Media.Brushes.DimGray;
            BtnRebel.Foreground = System.Windows.Media.Brushes.White;


            // 1. Create a random "coin flip" (0 or 1)
            int coinFlip = _rng.Next(0, 2);

            if (coinFlip == 0)
            {
                _player.AssignedRole = Role.Prisoner;
                _player.ID = "Prisoner #" + _rng.Next(100, 999);
                _player.Compliance = 50; // Start prisoners at a neutral 50%
                LogEvent($"ROLE ASSIGNED: You are {_player.ID}. Follow all guard instructions.");
            }
            else
            {
                _player.AssignedRole = Role.Guard;
                _player.ID = "Guard (Shift A)";
                _player.Compliance = 100; // Start guards at 100% authority alignment
                LogEvent("ROLE ASSIGNED: You are a GUARD. Maintain order at all costs.");
            }

            // 1. This hides the title screen grid we named in XAML
            TitleScreen.Visibility = Visibility.Collapsed;

            // 2. This adds the first official entry to your log
            LogEvent("Simulation Initialized. Subject #819 has entered the facility.");

            // 3. Make sure the progress bars show the player's starting stats
            UpdateUI();
            DisplayCurrentScenario();
        }

        
        private void DisplayCurrentScenario()
        {
            // 1. Check for custom End Game conditions before drawing a new scenario
            if (_player.MentalStability <= 0 || _player.Compliance <= 0 || _player.Compliance >= 100)
            {
                EvaluateExperimentEnding();
                return;
            }

            // 2. Grab a completely random scenario from our pool
            int randomIndex = _rng.Next(0, _timeline.Count);
            Scenario current = _timeline[randomIndex];

            // 3. Output narrative text
            LogEvent($"[EVENT] {current.Description}");

            // 4. Bind the button strings based on role
            if (_player.AssignedRole == Role.Prisoner)
            {
                BtnObey.Content = current.PrisonerChoiceA;
                BtnRebel.Content = current.PrisonerChoiceB;
            }
            else
            {
                BtnObey.Content = current.GuardChoiceA;
                BtnRebel.Content = current.GuardChoiceB;
            }
        }

        private void DisableButtons()
        {
            // Setting IsEnabled to false grays out the buttons and blocks clicks
            if (BtnObey != null) BtnObey.IsEnabled = false;
            if (BtnRebel != null) BtnRebel.IsEnabled = false;
            if (BtnEscape != null) BtnEscape.IsEnabled = false;

            LogEvent("[SYSTEM]: Simulation halted. Interaction disabled.");
        }
    }
}
    
