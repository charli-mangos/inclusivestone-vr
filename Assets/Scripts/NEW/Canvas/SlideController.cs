using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SlideController : MonoBehaviour
{
    //Slide panel
    public SlideUnit[] sld;
    private List<SlideUnit> slides;

    //Current slide active
    private SlideUnit currentSlide;

    //Current index active 
    private int index = 0;

    //Change Scene script
    public ChangeScene changeScene;
    public GameController gameController;

    //UI components
    public Text tittle;
    public Text description;
    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        if (slides == null || slides.Count == 0)
        {
            initSlider();

            setNextSlide();
        }

    }

    public void initSlider()
    {
        slides = sld.ToList<SlideUnit>();
    }

    public void setNextSlide()
    {
        if (currentSlide != null && index + 1 < slides.Count)
        {
            index = index + 1;
        }
        else
        {
            index = 0;
        }

        currentSlide = slides[index];

        setUIComponents();
    }

    public void setPreviousSlide()
    {
        if (index > 0)
        {
            index = index - 1;
        }
        else
        {
            index = slides.Count - 1;
        }

        setUIComponents();
    }

    public void setSlideByName(string name)
    {
        if(gameController != null)
        {
            checkSlides();
        }

        if (slides.Find(item => item.tittle == name) != null)
        {
            currentSlide = slides.Find(item => item.tittle == name);

            setUIComponents();
        }
        else
        {
            setNextSlide();
        }
    }

    public SlideUnit getSlideByName(string name)
    {
        return slides.Find(item => item.action == name);
    }

    public void removeSlide(SlideUnit sld)
    {
        slides.Remove(sld);
    }

    public void checkSlides()
    {
        //Chequeamos
        foreach (SlideUnit var in sld)
        {
            if (!gameController.uncompleted.Find(mis => mis.tittle == var.action) && var.tittle != "menu")
            {
                removeSlide(var);
            }
        }

        currentSlide = null;
    }

    public void setUIComponents()
    {
        description.text = "";

        if (img != null)
        {
            img.sprite = currentSlide.image;
        }

        if (tittle!=null)
        {
            tittle.text = MainTranslate.Fields[currentSlide.tittle];
        }

        if(currentSlide.selectButton != null)
        {
            currentSlide.selectButton.gameObject.SetActive(true);
            currentSlide.selectButton.Select();
        }

        if(currentSlide.startButton != null)
        {
            currentSlide.startButton.gameObject.SetActive(true);
        }

        if(changeScene != null && currentSlide.tittle == "menu")
        {
            currentSlide.startButton.onClick.AddListener(delegate { changeScene.change(currentSlide.action); });
        }
        else if(gameController != null && currentSlide.tittle != "menu")
        {
            currentSlide.startButton.onClick.AddListener(delegate { gameController.setMissionByName(currentSlide.action); });
        }

        foreach (var palID in currentSlide.description)
        {
            description.text = description.text + MainTranslate.Fields[palID] + "\n" + "\n";
        }
    }
}
