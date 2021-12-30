namespace saitho.Calculator
{
    internal class Calculator
    {
        protected static Calculator instance = null;

        public string operation { get; set; }
        Calculator() { }

        public static Calculator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Calculator();
                }
                return instance;
            }
        }


        public void reset()
        {
            this.operation = null;
        }

        public float getCurrentResult()
        {
            float storedNumber = 0;

            if (DataStorage.Instance.hasResultFile())
            {
                storedNumber = float.Parse(DataStorage.Instance.readResultFile());
            }
            return storedNumber;
        }

        public float getInput()
        {
            return CurrentNumberHolder.Instance.fullNumber;
        }

        public float performCalculation()
        {
            float newNumber = getCurrentResult();
            if (operation == "+")
            {
                newNumber += getInput();
            }
            else if (operation == "-")
            {
                newNumber -= getInput();
            }
            return newNumber;
        }
    }
}
