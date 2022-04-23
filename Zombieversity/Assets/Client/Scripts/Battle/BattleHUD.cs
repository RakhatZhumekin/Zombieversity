using UnityEngine;
using UnityEngine.UI;
public class BattleHUD : MonoBehaviour
{
   public Text NameText;
   public Slider HPSlider;

   public void SetHUD(Unit unit) {
       NameText.text = unit.UnitName;
       HPSlider.maxValue = unit.MaxHP;
       HPSlider.value = unit.CurrentHP;
   } 

   public void SetHP(int hp) {
       HPSlider.value = hp;
   }
}
