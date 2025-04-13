using System.Collections.Generic;
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
            public bool hasLight = false;
            public bool canPressurePlate;

            public string description
            {
                get {return hasLight ? conditionDescription : defaultDescription;}
            }
        }


        public ObjectData[] objects;
        public ObjectData[] heavyObjects;

        public ObjectData[]  InitializeObjects()
        {
            //add relevant objects in here, set their tags as interactable in the inspector
            objects = new ObjectData[]
            {
                new ObjectData { name = "SD_Prop_Shield_Rusty_01", defaultDescription = "I could use some light!", conditionDescription = "The knight should rest with his shield", canPressurePlate = true },
                new ObjectData { name = "SD_Prop_Sword_Rusty_01", defaultDescription = "I could use some light!" , conditionDescription = "The knight should rest with his sword", canPressurePlate = true},
                new ObjectData { name = "SD_Prop_Skull_Pile_01", defaultDescription = "I could use some light!", conditionDescription = "At least they have each other" },
                new ObjectData { name = "SD_Prop_Skull_Pile_02", defaultDescription = "I could use some light!", conditionDescription = "At least they have each other" },
                new ObjectData { name = "SD_Prop_Corpse_01", defaultDescription = "I could use some light!", conditionDescription = "Even if I move him, he will just turn to dust" },
                new ObjectData { name = "SD_Prop_Corpse_04", defaultDescription = "I wander why he didn't leave", conditionDescription = "That armor must be heavy", canPressurePlate = true},
                new ObjectData { name = "SD_Prop_CandleStand_01", defaultDescription = "I can use one of these", conditionDescription = "I already have one" },
                new ObjectData { name = "SD_Prop_CandleStand_02", defaultDescription = "I can use one of these", conditionDescription = "I already have one" },
                new ObjectData { name = "SD_Env_StoneCircle_01", defaultDescription = "I could use some light!", conditionDescription = "It looks like a... pressure plate?" },
            };
            return objects;
        }
        public void SetLightCondition()
        {
            foreach (var obj in objects)
            {
                obj.hasLight = true;
            }
        }
        public ObjectData[] GetHeavyObjects()
        {
            List<ObjectData> heavyObjectsList = new List<ObjectData>();
            foreach (var obj in objects)
            {
                if (obj.canPressurePlate)
                {
                    heavyObjectsList.Add(new ObjectData
                    {
                        name = obj.name,
                        defaultDescription = obj.defaultDescription,
                        conditionDescription = obj.conditionDescription,
                        hasLight = obj.hasLight,
                        canPressurePlate = obj.canPressurePlate
                    });
                }
            }
            return heavyObjectsList.ToArray();
        }
    }
}


