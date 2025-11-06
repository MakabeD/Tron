# `T R O N`

## Resumen

`TRON` Tron es un videojuego que acerca al jugador interactivamente al campo de la ciberseguridad. Es de tipo semi open-world y ambientado en una oficina de seguridad informática. El jugador encarna a Tron, un defensor de software que se encarga de atender y solucionar las anomalías y amenazas que puedan ocurrirle al sistema, sin importar sus métodos.

**(ve el final para aprender a jugar)**
### El problema que soluciona

La ciberseguridad es una de las ramas más complejas de la programación; esto no es un secreto para nadie. Con los minijuegos, se le da la oportunidad al jugador de enseñarle verdaderos casos de riesgo en un sistema por medio de interacciones con anomalías que las salas de la oficina puedan representar. Esto puede ayudarle al jugador interiorizar y afianzar los conceptos y casos que se pueden presentar en este juego.

---

## Integrantes y roles

Daniel Yepes Molina - Desarrollador principal y modelador 3D
Miguel Ángel Zapata Vargas - Desarrollador secundario, documentador, encargado de buscar y escoger renders y texturas para el arte visual del videojuego.

---

## Instrucciones para ejecutar el proyecto
---

## 1. Requisitos previos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

###  Software necesario
- **Unity Hub** (última versión disponible)
- **Unity Editor 2022.3.62f1**
- **Visual Studio 2022** (o Rider) con soporte para **.NET y C#**
- **Microsoft SQL Server** (versión 2019 o superior)
- **SQL Server Management Studio (SSMS)** (para administrar la base de datos)
- **Git** (opcional, si el proyecto está en un repositorio remoto)

---

##  2. Descarga del proyecto

### — Clonar el repositorio desde GitHub

Abre la terminal o Git Bash y ejecuta:

```bash
git clone https://github.com/usuario/nombre-del-proyecto.git
```
# 3. Abrir el proyecto en Unity Hub

Abre Unity Hub.

En la esquina superior derecha, haz clic en Add project o Agregar proyecto.

Navega hasta la carpeta donde descargaste o descomprimiste el proyecto.

Selecciona la carpeta y haz clic en Add Project.

Unity Hub detectará la versión del proyecto.

Si no tienes instalada la versión 2022.3.62f1, selecciona Download this version o Install manually desde Unity Hub.

Una vez instalada, selecciona Open para abrir el proyecto.

# 4. Ejecutar el proyecto en el Editor

Espera a qe Unity cargue todos los recursos del proyecto (Assets, Scripts, Scenes, etc.).

Esto puede tardar unos minutos la primera vez.

En el Panel del Proyecto (Project), abre la carpeta:

Assets/Scenes/

Busca la escena principal (en este caso para vos profe abris "Main(entrega para la profe)").

Haz doble clic sobre ella para abrirla.

Presiona el botón Play ▶️ en la parte superior del editor para ejecutar el proyecto.

---

## Enlace al script SQL (¿Oracle o SQL Server?)
Para acceer al script
[📂 Ir a la carpeta db](./db/)

---

# 🧠 Guía de Juego: Tron

## 🎯 Objetivo principal
Tu misión es **proteger el sistema informático** durante **6 horas del juego** (equivalente a unos **3 minutos en tiempo real**) evitando y resolviendo actividades sospechosas que amenazan la seguridad del sistema.

---

## 🏠 Escenario principal
Estás dentro de **una habitación principal** conectada por **un pasillo** que te lleva a **dos salas diferentes**.

Cada sala contiene un **cubito con un círculo**. Estos cubitos son **terminales de interacción** que te permiten acceder a **minijuegos** donde podrás **detectar y neutralizar amenazas**.

---

## ⚠️ Actividades sospechosas

- Cuando ocurra un evento, **aparecerá un mensaje en la parte superior de la pantalla** indicando que hay una **actividad sospechosa**.  
- Dirígete rápidamente a revisar **las dos salas**.
- La sala afectada se identifica porque **uno de sus focos estará encendido en color amarillo**.

### 🔍 Qué hacer:
1. Encuentra el **cubito con el círculo** en la sala con el foco amarillo.
2. Interactúa con él para **iniciar el minijuego** correspondiente.
3. Completa el minijuego con éxito para **resolver la amenaza**.

Si fallas o tardas demasiado, **entrarás en estado de emergencia**.

---

## 🚨 Estado de emergencia

El estado de emergencia ocurre cuando:
- Falla un minijuego.
- Te demoras mucho en resolver una amenaza.

Durante este estado:
- La sala afectada puede volverse peligrosa.
- Debes actuar rápido para **restablecer el sistema**.

---

## 🗡️ Cómo salir del estado de emergencia

1. Dirígete a **otro cubito con un ícono de espada en el centro** (también disponible en cada sala).
2. Interactúa con ese cubito para **iniciar un minijuego especial**.
3. Si lo completas exitosamente, **cancelas el estado de emergencia** y vuelves a la normalidad.

> 💡 Aún no hay combate real, pero este minijuego representa tu esfuerzo por restablecer la seguridad del sistema.

---

## ⏱️ Interfaz del juego

- **Tiempo restante:** esquina **superior izquierda** (simula las 6 horas del juego).
- **Vida del sistema:** esquina **superior derecha** (aún sin funcionalidad completa).
- **Eventos activos:** parte **superior central**, donde se muestran las **alertas de emergencia**.

---

## 🧩 Ciclo de juego

1. Espera a que aparezca una **actividad sospechosa**.
2. Busca la **sala con el foco amarillo**.
3. Interactúa con el **cubito con círculo** para resolverla.
4. Si fallas, entra en **estado de emergencia**.
5. Interactúa con el **cubito con espada** para salir del estado.
6. Repite el proceso hasta sobrevivir **las 6 horas**.

---

## 🧭 Consejos

- Manténte atento al **indicador de emergencia** en pantalla.
- Aprende a moverte rápido entre las salas.
- Memoriza la ubicación de los **cubitos de círculo y espada**.
- Cada segundo cuenta si quieres mantener el sistema a salvo.

---
## 🏁 Fin del juego
El juego termina cuando:
- Logras **sobrevivir las 6 horas completas**, 
Adicional recuerda que habra feedback finalizando las **6 horas del turno**

Tu recompensa es haber mantenido la **integridad del sistema informático** ante múltiples amenazas.
---
**Versión:** Pre-alpha 0.1  
--
