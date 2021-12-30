namespace saitho.Calculator
{
    internal class CurrentNumberHolder
    {
        protected static CurrentNumberHolder instance = null;
        public bool decimalMode { get; set; }

        string _currentNumber = "";

        public string currentNumber
        {
            get {
                string currentNumber = "";
                try
                {
                    currentNumber = int.Parse(_currentNumber).ToString();
                }
                catch
                {
                }
                return currentNumber;
            }
            set
            {
                _currentNumber = value;
            }
        }

        public string currentDecimalNumber { get; set; }

        public float fullNumber
        {
            get
            {
                float fullNumber = float.Parse(currentNumber);
                if (decimalMode)
                {
                    fullNumber += int.Parse(currentDecimalNumber) / (float)System.Math.Pow(10.0, currentDecimalNumber.Length);
                }
                return fullNumber;
            }
        }

        CurrentNumberHolder()
        {
        }

        public static CurrentNumberHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentNumberHolder();
                }
                return instance;
            }
        }

        public string add(string number)
        {
            if (decimalMode)
            {
                currentDecimalNumber += number;
                return currentNumber;
            }
            currentNumber += number;
            return currentNumber;
        }

        public void reset()
        {
            decimalMode = false;
            currentNumber = "";
            currentDecimalNumber = "";
        }
    }
}
