using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ParametersStructure
    {
        private Dictionary<string, string> enemiesParams = new Dictionary<string, string>();
        private Dictionary<string, string> environmentParams = new Dictionary<string, string>();
        private Dictionary<string, string> mainCharacterParams = new Dictionary<string, string>();

        public ParametersStructure() { }
        public ParametersStructure(Dictionary<string, string> enemiesParams, Dictionary<string, string> environmentParams, Dictionary<string, string> mainCharacterParams)
        {
            this.enemiesParams = enemiesParams;
            this.environmentParams = environmentParams;
            this.mainCharacterParams = mainCharacterParams;
        }

        public Dictionary<string, string> enemiesProp
        {
            get
            {
                return enemiesParams;
            }
            set
            {
                enemiesParams = value;
            }
        }

        public Dictionary<string, string> environmentProp
        {
            get
            {
                return environmentParams;
            }
            set
            {
                environmentParams = value;
            }
        }

        public Dictionary<string, string> mainCharacterProp
        {
            get
            {
                return mainCharacterParams;
            }
            set
            {
                mainCharacterParams = value;
            }
        }
    }

