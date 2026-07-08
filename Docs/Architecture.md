# Arquitectura

## Motor

- Motor: Unity 6
    
- Lenguaje: C#
    
- Pipeline de renderizado: Universal Render Pipeline (URP)
    
- Renderizador: 2D Renderer
    

---

# Resolución

- Resolución interna: 320 x 180 px
    
- Relación de aspecto: 16:9
    
- Cámara Pixel Perfect
    
- Escalado entero para preservar la nitidez del pixel art.
    

---

# Dirección técnica

El proyecto se desarrollará respetando los assets existentes.

No se crearán nuevas animaciones ni ilustraciones durante el desarrollo.

Las mecánicas se adaptarán a los recursos disponibles.

---

# Organización del proyecto

```
Assets/

Animations/
Art/
Audio/
Materials/
Prefabs/
Scenes/
Scripts/
Settings/
UI/
VFX/
```

La documentación del proyecto permanecerá fuera de Unity, dentro de la carpeta `Docs`.

---

# Arquitectura general

El proyecto estará dividido en sistemas independientes.

Cada sistema tendrá una única responsabilidad.

Ejemplos:

- Jugador
    
- Armas
    
- Proyectiles
    
- Enemigos
    
- IA
    
- Cámara
    
- Interfaz
    
- Audio
    
- Guardado
    

Los sistemas deberán comunicarse mediante interfaces o eventos cuando sea posible, evitando dependencias innecesarias.

---

# Estructura del jugador

El personaje estará compuesto por varios elementos independientes.

```
Player

├── Body
├── Arms
├── Weapon
├── Fire Point
├── Animator
├── Audio
└── VFX
```

Los brazos y las armas serán elementos independientes para permitir reutilizar las mismas animaciones con distintos tipos de armamento.

---

# Sistema de armas

Las armas serán objetos independientes del jugador.

Cada arma definirá su propio comportamiento.

Ejemplos:

- daño
    
- cadencia
    
- alcance
    
- tipo de proyectil
    
- capacidad del cargador
    
- munición máxima
    
- efectos visuales
    
- sonidos
    

El jugador únicamente equipa el arma activa.

---

# Escenas

El desarrollo comenzará con una escena de pruebas denominada **Sandbox**.

En esta escena se implementarán y validarán todas las mecánicas antes de incorporarlas a los niveles del juego.

---

# Principios de desarrollo

- Priorizar código simple y legible.
    
- Evitar optimizaciones prematuras.
    
- Diseñar sistemas reutilizables.
    
- Desarrollar una mecánica a la vez.
    
- Completar cada sistema antes de comenzar el siguiente.
    
- Mantener la documentación sincronizada con el proyecto.
    

---

# Convenciones

- Scripts en inglés.
    
- Variables y métodos siguiendo las convenciones de C#.
    
- Documentación en español.
    
- Commits pequeños y descriptivos.
    
- Una característica funcional por commit siempre que sea posible.