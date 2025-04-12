using UnityEngine;

namespace AE
{
    [CreateAssetMenu(fileName = "NewInteractableObjects", menuName = "AE/InteractableObjects")]
    public class InteractableObjects : ScriptableObject
    {
        [System.Serializable]
        public class ObjectData
        {
            public string name;
            public string defaultDescription;
            public string conditionDescription;
            public bool conditionMet;
            public string description
            {
                get
                {
                    return conditionMet ? conditionDescription : defaultDescription;
                }
            }
        }


        public ObjectData[] Objects;

        public ObjectData[]  InitializeObjects()
        {
            //add relevant objects in here, set their tags as interactable in the inspector
            Objects = new ObjectData[]
            {
                new ObjectData { name = "SD_Prop_Shield_Rusty_01", defaultDescription = "Description of Object1" },
                new ObjectData { name = "SD_Prop_Sword_Rusty_01", defaultDescription = "Description of Object2" },
                new ObjectData { name = "SD_Prop_Skull_Pile_01", defaultDescription = "Description of Object3" },
                new ObjectData { name = "SD_Prop_Skull_Pile_02", defaultDescription = "Description of Object4 " },
                new ObjectData { name = "SD_Prop_Corpse_01", defaultDescription = "dfasdf" },
                new ObjectData { name = "SD_Prop_Corpse_04", defaultDescription = "Stabing knight in his chestplate? He must have comitted suicide, or get jumped on" },
                new ObjectData { name = "SD_Prop_CandleStand_01", defaultDescription = "I can use one of these", conditionDescription = "I already have one" },
                new ObjectData { name = "SD_Prop_CandleStand_02", defaultDescription = "I can use one of these", conditionDescription = "I already have one" },
                new ObjectData { name = "SD_Prop_Bench_Broken_01", defaultDescription = "ain't no sitting around here" }
            };
            return Objects;
        }

        public void SetCondition(string objectName, bool conditionMet)
        {
            var obj = System.Array.Find(Objects, o => o.name == objectName);
            if (obj != null)
            {
                obj.conditionMet = conditionMet;
            }
        }
    }
}


