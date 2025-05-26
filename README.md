Este proyecto demuestra cómo implementar pruebas unitarias en un entorno .NET usando **xUnit**, con el uso de **Mocks (Moq)** y **Stubs manuales** para asegurar el correcto comportamiento de los metodos de servicio de estudiantes.

## Objetivos del proyecto

1. Crear un proyecto de pruebas unitarias en .NET con xUnit.
2. Probar el método `HasApproved` del servicio `StudentService`.
3. Simular el comportamiento de `StudentService` usando:
   - Mocks (con Moq)
   - Un Stub manual
4. Verificar distintos casos:
   - Estudiante con nota mayor a 51 (debe aprobar)
   - Estudiante con nota menor a 51 (no debe aprobar)
   - Validar que el CI y nombre del estudiante sean los ingresados

## Enfoques usados

### ✔️ Pruebas con Mock (Moq)

- Se probó todo el CRUD de estudiantes a manera de práctica.
- También se verificó el método `HasApproved` con múltiples combinaciones:
  - Basado en CI
  - Basado en nombre
  - Basado en el objeto `Student`
- Se utilizó esta estructura:

```csharp
private readonly Mock<IStudentService> _mockStudentService;
private readonly StudentController _controller;

public StudentControllerTestsUseMock()
{
    _mockStudentService = new Mock<IStudentService>();
    _controller = new StudentController(_mockStudentService.Object);
}
````
> **Importante mencionar** que xUnit ejecuta el constructor antes de cada test, garantizando **independencia entre pruebas** evitando codigo repetido y manteniendo el estado limpio.

---

### ✔️ Pruebas con Stub Manual

* Se probó el método `GetStudentByCI`, `CreateStudent` y `HasApproved`.
* Se validaron todos los casos requeridos de aprobación y datos del estudiante.
* Estructura utilizada:

```csharp
private readonly StudentServiceStub _studentServiceStub;
private readonly StudentController _controller;

public StudentControllerTestsUseStub()
{
    _studentServiceStub = new StudentServiceStub();
    _controller = new StudentController(_studentServiceStub);
}
```

> También aquí, **xUnit reinicia el estado por test**, manteniendo la independencia sin instanciar manualmente por cada caso de prueba.

---

## Conclusión

Este proyecto demuestra dos formas efectivas de testear logica local de servicios en .NET:

* **Mocks** para aislar comportamientos.
* **Stubs** como alternativa ligera, especialmente útil en pruebas con lógica predecible.

