![UCU](https://github.com/ucudal/PII_Conceptos_De_POO/raw/master/Assets/logo-ucu.png)

## FIT - Universidad Católica del Uruguay

### Programación II

# API de Twitter

API para publicar en la cuenta de Twitter del curso [@POOUCU](https://twitter.com/pooucu) y enviar mensajes directos.

## Publicar en Twitter
Para poder publicar en Twitter, una vez agregada la libreria a su proyecto como referencia,
podrán hacer uso del siguiente código de ejemplo:

```c#
var twitter = new TwitterImage();
Console.WriteLine(twitter.PublishToTwitter("text", @"PathToImage.png"));
```

Esto publicará en la cuenta @POOUCU la imagen y texto enviados e imprime por consola el resultado de la publicación. En caso de ser correcta, imprime "OK".

> Los repositorios que usan esta librería asumen que fueron descargados en la misma carpeta 'madre'.
