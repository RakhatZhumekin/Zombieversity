using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
   public Text NameText;
   public Text LevelText;
   public Slider HPSlider;

   public void SetHUD(Unit unit) {
       NameText.text = unit.UnitName;
       LevelText.text = "Lvl " + unit.UnitLevel;
       HPSlider.maxValue = unit.MaxHP;
       HPSlider.value = unit.CurrentHP;
   } 

   public void SetHP(int hp) {
       HPSlider.value = hp;
   }
}
