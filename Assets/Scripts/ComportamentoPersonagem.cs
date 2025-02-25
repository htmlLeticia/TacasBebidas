using UnityEngine;

public class ComportamentoPersonagem : MonoBehaviour
{
    public float velocidadePersonagem, forcaDoPulo;
    
    // Update is called once per frame
    void Update()
    {
        //este código é para fazer o persoagem ir para a direita
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 novaPos = new Vector3(velocidadePersonagem * Time.deltaTime, 0, 0);
            transform.localPosition += novaPos;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //este código é para fazer o persoagem ir para a esquerda
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 novaPos = new Vector3(velocidadePersonagem * Time.deltaTime, 0, 0); // variavel que guarda os valores de x, y e z -  para trabalhar com mundo 3D
            transform.localPosition -= novaPos; //é a posição local refente ao pai
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }


        //código para o personagem pular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.GetComponent<Rigidbody2D>().linearVelocity.y == 0)
            {
                Vector2 sentidoCima = new Vector2(0, 1);
                gameObject.GetComponent<Rigidbody2D>().AddForce(sentidoCima * forcaDoPulo);
            }
        }
    }
}
