using Game.Core.Grid;
using Game.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class InterfaceManager : MonoBehaviour, IProvidable
    {
        [Header("References")]
        [SerializeField] private Slider gridSlider;
        [SerializeField] private TMP_Text gridSliderTxt;
    
        [Header("Properties")]
        [SerializeField] private int sliderMinValue = 2;
        [SerializeField] private int sliderMaxValue = 100;
        
        private GridSystem grid;
    
        public int GetSliderMaxValue => sliderMaxValue;
    
        private void Awake()
        {
            ServiceProvider.Register(this);
            grid = FindObjectOfType<GridSystem>();
        }
    
        private void Start()
        {
            InitializeGridSlider();
        }
    
        private void InitializeGridSlider()
        {
            gridSlider.minValue = sliderMinValue;
            gridSlider.maxValue = sliderMaxValue;
            gridSlider.SetValueWithoutNotify(grid.Size);
            UpdateGridSliderText(grid.Size);
        }
    
        public void UpdateGridOnSliderChange()
        {
            int newSize = (int)gridSlider.value;
            grid.Size = newSize;
            UpdateGridSliderText(newSize);
    
            grid.UpdateGrid();
        }
        
        public void UpdateGridSizeBySum(int integer)
        {
            int newSize = grid.Size + integer;
            newSize = Mathf.Clamp(newSize, sliderMinValue, sliderMaxValue);
            grid.Size = newSize;
            gridSlider.SetValueWithoutNotify(newSize);
            UpdateGridSliderText(newSize);
            
            grid.UpdateGrid();
        }
    
        private void UpdateGridSliderText(int size)
        {
            gridSliderTxt.text = size.ToString();
        }
    }
}
