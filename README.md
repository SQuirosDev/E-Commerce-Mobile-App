# 📱 Aplicación Móvil de E-Commerce

Proyecto **universitario** desarrollado como parte de la materia **Programación Móvil (Programación 3)**.  
El objetivo principal del proyecto fue diseñar y desarrollar una **aplicación móvil de E-Commerce** utilizando una **arquitectura por capas**, integrando base de datos, backend y frontend.

Este proyecto representa uno de los desarrollos **más completos y avanzados** realizados durante la carrera, tanto por su arquitectura como por la cantidad de funcionalidades implementadas.

---

## 🎯 Objetivo del Proyecto

Desarrollar un sistema de E-Commerce en una aplicación móvil que permita la gestión de usuarios, productos e interacciones, incorporando funcionalidades avanzadas como notificaciones por correo, comentarios, chat en tiempo real y el intento de integración de inteligencia artificial y mapas.

---

## 🧱 Arquitectura del Proyecto

El proyecto fue diseñado bajo una **arquitectura por capas**, donde cada capa se encuentra completamente separada y organizada en su propio proyecto.

### 🔹 Base de Datos
- Implementada en **SQL Server**
- Uso de **tablas relacionales**
- Procedimientos almacenados (SP) para:
  - Inserción de datos
  - Obtención de datos

### 🔹 Backend
- Desarrollado en **.NET**
- Acceso a datos mediante **LINQ**
- Exposición de funcionalidades a través de una **REST API**
- Funciones principales:
  - Lógica de negocio
  - Comunicación con la base de datos
  - Envío de notificaciones por correo
  - Intento de integración con inteligencia artificial (Algolia)
- La API funciona como intermediario entre el frontend y la base de datos

### 🔹 Frontend
- Aplicación móvil desarrollada en **.NET MAUI**
- Conexión directa con la API del backend
- Diseño visual cuidado y moderno
- Funcionalidades:
  - Ingreso y visualización de datos
  - Navegación fluida
  - Experiencia de usuario intuitiva

**Compatibilidad y despliegue**
- Funcionamiento completo en **Windows Machine**
- En el **emulador de Android** se presentaron inconvenientes relacionados con la IP y la comunicación con los endpoints de la API
- Al realizar el **despliegue del backend y la base de datos en Microsoft Azure**, la aplicación funcionó correctamente en un **dispositivo Android real**, permitiendo funcionamiento en un entorno móvil real

📌 En cada una de las capas se implementaron **validaciones de datos** para garantizar la integridad de la información.

---

## 🛠️ Tecnologías Utilizadas

### Lenguajes
- SQL (Transact-SQL)
- C#
- XAML

### Frameworks / Librerías
- .NET
- .NET MAUI

### Herramientas
- SQL Server
- Visual Studio
- Microsoft Azure

---

## ⚙️ Funcionalidades Principales

### Usuarios
- Registro de usuario
- Inicio de sesión
- Verificación de correo electrónico
- Cambio de contraseña
- Obtención de información del usuario
- Cambio de rol
- Cierre de sesión

### Productos
- Ingreso de productos
- Obtención de producto
- Listado de productos
- Listado de productos filtrados
- Listado de productos recomendados
- Historial del último producto visto

### Interacción
- Ingreso de comentarios
- Listado de comentarios
- Sistema de chat

---

## 📈 Nivel del Proyecto

**Avanzado**

---

## 📌 Notas Adicionales y Alcance

Este fue mi **quinto proyecto programado** y el **tercer proyecto grande y complejo** desarrollado en el ámbito universitario, involucrando una arquitectura completa con **Base de Datos, Backend y Frontend**.

Durante su desarrollo, el proyecto fue **desplegado en Microsoft Azure**, lo que permitió publicar la base de datos, el backend y la API para utilizar la aplicación desde un **teléfono Android real**, validando su funcionamiento fuera del entorno local.

El proyecto fue desarrollado durante un **cuatrimestre**, lo que implicó definir prioridades y alcance. Algunas funcionalidades avanzadas presentaron mayor complejidad técnica:

- **Chats:**  
  Se implementó comunicación en tiempo real utilizando **SignalR**, logrando un chat general. La implementación de chats individuales entre cliente y vendedor quedó fuera del alcance por tiempo.

- **Inteligencia Artificial:**  
  Se logró enviar información hacia la IA, pero no fue posible obtener respuestas. Además, la documentación de **Algolia para C#** presentaba limitaciones que dificultaron su integración completa.

- **Mapas:**  
  Se investigó la integración mediante **MAUI Maps** y la API de **Google Maps**, pero la aplicación presentaba fallos al integrarlos y no se contó con el tiempo suficiente para resolverlo.

- **CRUD completo:**  
  Algunas operaciones adicionales típicas de un CRUD no fueron implementadas debido a la gran cantidad de funcionalidades ya incluidas y al tiempo limitado del cuatrimestre.

📌 **Nota sobre el video demostrativo:**  
El video fue grabado aproximadamente **un año después** del desarrollo del proyecto. En ese momento, la cuenta de **Azure de estudiante** ya no se encontraba activa, por lo que el demo muestra el funcionamiento completo en **Windows Machine** y una visualización parcial en el **emulador de Android**.

Proyecto desarrollado en equipo junto a:
- Sebastián Quirós
- Ignacio Mejia
- Angelo Valdivia

---

## 📂 Repositorio
🔗 https://github.com/SQuirosDev/E-Commerce-Mobile-App
---

## 🎥 Video Demostrativo
🔗 https://youtu.be/yXA1ESIHlZw?si=0_AayB2y4VZIng3z
