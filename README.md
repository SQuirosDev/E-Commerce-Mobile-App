> ⚠️ **Important Notice**  
> The backend and frontend source code are currently not available in this repository due to technical issues encountered during the upload process.  
>  
> The repository will be updated as soon as these issues are resolved.
> 
> ⚠️ **Aviso Importante**  
> El código fuente del backend y del frontend no se encuentra disponible actualmente en este repositorio debido a inconvenientes técnicos durante el proceso de subida.  
>  
> El repositorio será actualizado tan pronto como estos inconvenientes sean resueltos.

---

---

# 📱 Mobile E-Commerce Application

**University project** developed as part of the course **Mobile Programming (Programming 3)**.  
The main objective of this project was to design and develop a **mobile E-Commerce application** using a **layered architecture**, integrating database, backend, and frontend.

This project represents one of the **most complete and advanced developments** carried out during the degree program, both in terms of architecture and the number of implemented features.

---

## 🎯 Project Objective

To develop a mobile E-Commerce system that allows the management of users, products, and interactions, incorporating advanced functionalities such as email notifications, comments, real-time chat, and attempted integration of artificial intelligence and maps.

---

## 🧱 Project Architecture

The project was designed using a **layered architecture**, where each layer is fully separated and organized into its own project.

### 🔹 Database
- Implemented using **SQL Server**
- Use of **relational tables**
- Stored Procedures (SP) for:
  - Data insertion
  - Data retrieval

### 🔹 Backend
- Developed using **.NET**
- Data access through **LINQ**
- Feature exposure via a **REST API**
- Main responsibilities:
  - Business logic
  - Database communication
  - Email notification sending
  - Attempted integration with artificial intelligence (Algolia)
- The API acts as an intermediary between the frontend and the database

### 🔹 Frontend
- Mobile application developed using **.NET MAUI**
- Direct connection to the backend API
- Clean and modern UI design
- Features:
  - Data input and visualization
  - Smooth navigation
  - Intuitive user experience

**Compatibility and deployment**
- Fully functional on **Windows Machine**
- Issues were encountered on the **Android emulator**, mainly related to IP configuration and API endpoint communication
- After deploying the **backend and database to Microsoft Azure**, the application worked correctly on a **real Android device**, validating proper functionality in a real mobile environment

📌 **Data validation** was implemented across all layers to ensure data integrity.

---

## 🛠️ Technologies Used

### Languages
- SQL (Transact-SQL)
- C#
- XAML

### Frameworks / Libraries
- .NET
- .NET MAUI

### Tools
- SQL Server
- Visual Studio
- Microsoft Azure

---

## ⚙️ Main Features

### Users
- User registration
- User login
- Email verification
- Password change
- User information retrieval
- Role management
- User logout

### Products
- Product creation
- Product retrieval
- Product listing
- Filtered product listing
- Recommended products
- Last viewed product history

### Interaction
- Comment submission
- Comment listing
- Chat system

---

## 📈 Project Level

**Advanced**

---

## 📌 Additional Notes and Scope

This was my **fifth programmed project** and the **third large and complex university project**, involving a complete architecture with **Database, Backend, and Frontend**.

During development, the project was **deployed to Microsoft Azure**, allowing the database, backend, and API to be published and accessed from a **real Android device**, validating functionality outside the local environment.

The project was developed over the course of a **single academic term**, which required prioritization and scope definition. Some advanced features involved higher technical complexity:

- **Chats:**  
  Real-time communication was implemented using **SignalR**, achieving a general chat system. Individual chats between customers and sellers were out of scope due to time constraints.

- **Artificial Intelligence:**  
  Data was successfully sent to the AI service; however, responses could not be retrieved. Additionally, **Algolia's C# documentation** presented limitations that hindered full integration.

- **Maps:**  
  Integration was explored using **MAUI Maps** and the **Google Maps API**, but application crashes occurred during implementation, and time constraints prevented further resolution.

- **Complete CRUD:**  
  Some additional CRUD operations were not implemented due to the large number of existing features and limited development time.

📌 **Demo video note:**  
The demo video was recorded approximately **one year after** the project was developed. At that time, the **Azure student account** was no longer active, so the demo showcases full functionality on **Windows Machine** and a partial demonstration on the **Android emulator**.

Project developed as a team with:
- Sebastián Quirós
- Ignacio Mejia
- Angelo Valdivia

---

## 📂 Repository
🔗 https://github.com/SQuirosDev/E-Commerce-Mobile-App

---

## 🎥 Demo Video
🔗 https://youtu.be/yXA1ESIHlZw?si=0_AayB2y4VZIng3z

---

## 👥 Team Contributions

The project was developed as a team, with clearly defined responsibilities across each layer, feature, and deployment process:

- **Database:**  
  Ignacio Mejia  
  Design and implementation of the SQL Server database, including table creation, stored procedures, and support for map integration.

- **Backend and API Development:**  
  Sebastián Quirós  
  Backend development using .NET, REST API creation, business logic implementation, data validations, data access using LINQ, email notification handling, and artificial intelligence integration.

- **Frontend (logic, validations, and API integration):**  
  Sebastián Quirós  
  Implementation of frontend logic using .NET MAUI, data validations, and API consumption.

- **Frontend (views and visual design):**  
  Sebastián Quirós & Angelo Valdivia  
  Visual design of the application, creation of MAUI views, and development of the real-time chat system using SignalR.

- **Deployment:**  
  Sebastián Quirós  
  Deployment of the SQL Server database and backend API to **Microsoft Azure**, configuration of public endpoints, and installation of the application on a **real Android device**.

- **Additional Features:**  
  - **Maps:** Ignacio Mejia  
  - **Artificial Intelligence:** Sebastián Quirós  
  - **Real-time Chats:** Angelo Valdivia

---

---

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

---

## 👥 Aportes del Equipo

El proyecto fue desarrollado en equipo, con responsabilidades claramente definidas en cada capa, funcionalidad y proceso de despliegue:

- **Base de datos:**  
  Ignacio Mejia  
  Diseño e implementación de la base de datos en SQL Server, incluyendo la creación de tablas, procedimientos almacenados y apoyo en la integración de mapas.

- **Backend y desarrollo de la API:**  
  Sebastián Quirós  
  Desarrollo del backend en .NET, creación de la API REST, implementación de la lógica de negocio, validaciones, acceso a datos mediante LINQ, envío de notificaciones por correo e integración de inteligencia artificial.

- **Frontend (lógica, validaciones y conexión con la API):**  
  Sebastián Quirós  
  Implementación de la lógica del frontend en .NET MAUI, validaciones de datos y consumo de la API.

- **Frontend (vistas y diseño visual):**  
  Sebastián Quirós y Angelo Valdivia  
  Diseño visual de la aplicación, creación de vistas en MAUI y desarrollo del sistema de chat en tiempo real utilizando SignalR.

- **Despliegue:**  
  Sebastián Quirós  
  Despliegue de la base de datos SQL Server y la API del backend en **Microsoft Azure**, configuración de endpoints públicos y la instalacion de la aplicación en un **dispositivo Android real**.

- **Funcionalidades adicionales:**  
  - **Mapas:** Ignacio Mejia  
  - **Inteligencia Artificial:** Sebastián Quirós  
  - **Chats en tiempo real:** Angelo Valdivia
