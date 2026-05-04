# Patrón de Diseño Estado — Aire Acondicionado Inverter

> **Instituto Tecnológico de Tijuana**  
> Ingeniería en Sistemas Computacionales  
> Materia: Patrones de Diseño  
> Alumno: Gonzalo Cortez Huerta · Número de control: 22210761

---

## ¿Qué es el Patrón de Diseño Estado?

Es un patrón de comportamiento que permite a un objeto **cambiar su comportamiento cuando cambia su estado interno**, como si el objeto cambiara de clase en tiempo de ejecución.

En lugar de usar múltiples `if/else` o `switch` para controlar el comportamiento según el estado, cada estado se encapsula en su **propia clase**.


---

## Ventaja

**Fácil de extender sin modificar el código existente.**  
Si se necesita agregar un nuevo modo (por ejemplo, modo *Turbo*), solo se crea una nueva clase `EstadoTurbo` que herede de `EstadoAC`. No es necesario modificar ninguno de los estados existentes.

---

## Desventaja

**Aumenta el número de clases del proyecto.**  
Cada estado requiere su propio archivo y clase. En sistemas con muchos estados esto puede volverse difícil de mantener, especialmente si los comportamientos entre estados son muy similares.

---

## Diagrama de Estados

[![Patron-de-Diseno-Estado-(3).png](https://i.postimg.cc/mD95gyv1/Patron-de-Diseno-Estado-(3).png)](https://postimg.cc/Q9Xb61xj)

## Programa ejecutado

[![Captura-de-pantalla-2026-05-03-202051.png](https://i.postimg.cc/bJRM0r18/Captura-de-pantalla-2026-05-03-202051.png)](https://postimg.cc/WFhXT29f)
