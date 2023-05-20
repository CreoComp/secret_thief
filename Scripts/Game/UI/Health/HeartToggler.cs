using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeartToggler : MonoBehaviour
{
    private Image _image;
    [SerializeField] private int _hpRemaining;
    public int HpRemaining { get => _hpRemaining; set => _hpRemaining = value; }
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        SetAlive(true);
    }

    public void SetAlive(bool isAlive)
    {
        _image.sprite = isAlive ? _fullHeart : _emptyHeart;
    }


}
