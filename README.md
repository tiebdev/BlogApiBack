# 📝 Blog API - Gestión de Blog con Autenticación JWT & MongoDB

Bienvenido a **Blog API**, una API REST diseñada para gestionar un blog privado con autenticación segura y control de acceso basado en JWT.  
Esta API permite la creación, edición y eliminación de posts, comentarios y el almacenamiento de imágenes, videos y archivos adjuntos.

## 🔥 Características  
✅ **Autenticación y Autorización JWT** - Solo colaboradores autorizados pueden registrarse y acceder.  
✅ **Gestión de Posts** - Creación, edición, eliminación y filtrado por autor.  
✅ **Comentarios en Posts** - Asociación de comentarios a los posts con metadatos del autor.  
✅ **Soporte para Multimedia** - Almacenamiento de imágenes, videos y archivos en los posts.  
✅ **CORS Configurado** - Permite el acceso desde un frontend externo.  
✅ **Registro de Logs con ILogger** - Para diagnóstico y monitoreo de errores.  
✅ **Swagger para Documentación** - Interfaz interactiva para probar los endpoints.  

## 🚀 Tecnologías Utilizadas  
- **C# & .NET 6** - Framework principal para la API.  
- **ASP.NET Core** - Para la construcción de la API REST.  
- **MongoDB** - Base de datos NoSQL para almacenamiento de posts y usuarios.  
- **JWT (JSON Web Token)** - Para autenticación y autorización segura.  
- **Swagger** - Documentación interactiva y pruebas de endpoints.  
- **CORS** - Configuración de seguridad para peticiones externas.  

## 📌 Endpoints Principales  
### 🔹 Autenticación  
- `POST /api/auth/register` → Registro de colaboradores autorizados.  
- `POST /api/auth/login` → Inicio de sesión y generación de token JWT.  

### 🔹 Gestión de Posts  
- `POST /api/posts` → Crear un nuevo post.  
- `GET /api/posts` → Obtener todos los posts.  
- `GET /api/posts/{id}` → Obtener un post específico.  
- `GET /api/posts/byauthor/{authorId}` → Obtener posts de un autor.  
- `PUT /api/posts/{id}` → Actualizar un post existente.  
- `DELETE /api/posts/{id}` → Eliminar un post.  

### 🔹 Comentarios  
- `POST /api/posts/{postId}/comments` → Agregar un comentario a un post.  

## 🔧 Instalación y Uso  
### 1️⃣ Clonar el Repositorio  
```bash
git clone https://github.com/tiebdev/BlogApiBack
cd blog-api
```
### 2️⃣ Configurar la Base de Datos  
Asegúrate de tener **MongoDB** corriendo en tu máquina o usa un servicio en la nube como MongoDB Atlas.  

En `appsettings.json`, configura la conexión:  
```json
"MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "prueba"
}
```
### 3️⃣ Configurar JWT  
Define una clave segura en `appsettings.json`:  
```json
"Jwt": {
    "Key": "tu_clave_segura_aqui",
    "Issuer": "techblog",
    "Audience": "techpeople"
}
```
### 4️⃣ Ejecutar la API  
```bash
dotnet run
```
La API se ejecutará en **https://localhost:7097** y podrás probar los endpoints en **Swagger** en:  
🔗 `https://localhost:7097/swagger/index.html`  

## 📬 Contacto  
📩 **Email:** [ridouan@tieb.dev](mailto:ridouan@tieb.dev)  
🔗 **LinkedIn:** [linkedin.com/in/ridouantieb](https://linkedin.com/in/ridouantieb)  

---

⚡ *Si te gustó este proyecto, ¡no olvides darle una ⭐ en GitHub!* 🚀  
