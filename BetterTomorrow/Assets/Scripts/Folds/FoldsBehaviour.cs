using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldsBehaviour : MonoBehaviour
{
    public GameObject film1;
    public GameObject film2;

    public int showFilmSortingOrder = 5;
    public int hideFilmSortingOrder = -2;

    public float filmSpeed = 3f;

    private SpriteRenderer film1SpriteRenderer;
    private SpriteRenderer film2SpriteRenderer;

    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        film1SpriteRenderer = film1.GetComponent<SpriteRenderer>();
        film2SpriteRenderer = film2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetFilm1LocalPosition()
    {
        return film1.transform.localPosition;
    }

    public Vector3 GetFilm2LocalPosition()
    {
        return film2.transform.localPosition;
    }

    public void ShowFilms()
    {
        film1SpriteRenderer.sortingOrder = showFilmSortingOrder;
        film2SpriteRenderer.sortingOrder = showFilmSortingOrder;

        film1.gameObject.SetActive(true);
        film2.gameObject.SetActive(true);
    }

    public void HideFilms()
    {
        direction = 1;

        film1SpriteRenderer.sortingOrder = hideFilmSortingOrder;
        film2SpriteRenderer.sortingOrder = hideFilmSortingOrder;

        film1.gameObject.SetActive(false);
        film2.gameObject.SetActive(false);
    }

    public void MoveFilms()
    {
        float film1Y;
        float film2Y;

        float delta = filmSpeed * Time.fixedDeltaTime * direction;

        film1.transform.position = new Vector2(film1.transform.position.x, film1.transform.position.y - delta);
        film2.transform.position = new Vector2(film2.transform.position.x, film2.transform.position.y + delta);
    }

    public void InvertDirection()
    {
        direction = direction * -1;
    }
}
