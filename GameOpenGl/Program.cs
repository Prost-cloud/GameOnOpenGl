using System;
using GLFW;
using OpenGL;

class Program
{

    /// <summary>
    /// Obligatory name for your first OpenGL example program.
    /// </summary>
    private const string TITLE = "Hello Triangle!";

    static void Main(string[] args)
    {
        // Set context creation hints
        PrepareContext();
        // Create a window and shader program
        var window = CreateWindow(200, 200);
        var program = CreateProgram();

        // Define a simple triangle
        CreateVertices(out var vao, out var vbo);
        rand = new Random();

        var location = GL.glGetUniformLocation(program, "color");
        SetRandomColor(location);
        long n = 0;

        while (!Glfw.WindowShouldClose(window))
        {
            // Swap fore/back framebuffers, and poll for operating system events.
            Glfw.SwapBuffers(window);
            Glfw.PollEvents();

            // Clear the framebuffer to defined background color
            GL.glClear(GL.GL_COLOR_BUFFER_BIT);

            if (n++ % 60 == 0)
                SetRandomColor(location);

            // Draw the triangle.
            GL.glDrawArrays(GL.GL_TRIANGLES, 0, 3);
            //glDrawArraysInstanced(GL_TRIANGLE_FAN, 0, 4, 3);

        }

        Glfw.Terminate();
    }

    private static void SetRandomColor(int location)
    {
        var r = (float)rand.NextDouble();
        var g = (float)rand.NextDouble();
        var b = (float)rand.NextDouble();
        GL.glUniform3f(location, r, g, b);
    }

    private static void PrepareContext()
    {
        // Set some common hints for the OpenGL profile creation
        Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
        Glfw.WindowHint(Hint.Doublebuffer, true);
        Glfw.WindowHint(Hint.Decorated, true);
    }

    /// <summary>
    /// Creates and returns a handle to a GLFW window with a current OpenGL context.
    /// </summary>
    /// <param name="width">The width of the client area, in pixels.</param>
    /// <param name="height">The height of the client area, in pixels.</param>
    /// <returns>A handle to the created window.</returns>
    private static Window CreateWindow(int width, int height)
    {
        // Create window, make the OpenGL context current on the thread, and import graphics functions
        var window = Glfw.CreateWindow(width, height, TITLE, GLFW.Monitor.None, Window.None);

        // Center window
        var screen = Glfw.PrimaryMonitor.WorkArea;
        var x = (screen.Width - width) / 2;
        var y = (screen.Height - height) / 2;
        Glfw.SetWindowPosition(window, x, y);

        Glfw.MakeContextCurrent(window);
        GL.Import(Glfw.GetProcAddress);



        return window;
    }

    /// <summary>
    /// Creates an extremely basic shader program that is capable of displaying a triangle on screen.
    /// </summary>
    /// <returns>The created shader program. No error checking is performed for this basic example.</returns>
    private static uint CreateProgram()
    {
        var vertex = CreateShader(GL.GL_VERTEX_SHADER, @"#version 330 core
                                                    layout (location = 0) in vec3 pos;

                                                    void main()
                                                    {
                                                        gl_Position = vec4(pos.x, pos.y, pos.z, 1.0);
                                                    }");
        var fragment = CreateShader(GL.GL_FRAGMENT_SHADER, @"#version 330 core
                                                        out vec4 result;

                                                        uniform vec3 color;

                                                        void main()
                                                        {
                                                            result = vec4(color, 1.0);
                                                        } ");

        var program = GL.glCreateProgram();
        GL.glAttachShader(program, vertex);
        GL.glAttachShader(program, fragment);

        GL.glLinkProgram(program);

        GL.glDeleteShader(vertex);
        GL.glDeleteShader(fragment);

        GL.glUseProgram(program);
        return program;
        //GL.glFlush();
    }

    /// <summary>
    /// Creates a shader of the specified type from the given source string.
    /// </summary>
    /// <param name="type">An OpenGL enum for the shader type.</param>
    /// <param name="source">The source code of the shader.</param>
    /// <returns>The created shader. No error checking is performed for this basic example.</returns>
    private static uint CreateShader(int type, string source)
    {
        var shader = GL.glCreateShader(type);
        GL.glShaderSource(shader, source);
        GL.glCompileShader(shader);
        return shader;
    }

    /// <summary>
    /// Creates a VBO and VAO to store the vertices for a triangle.
    /// </summary>
    /// <param name="vao">The created vertex array object for the triangle.</param>
    /// <param name="vbo">The created vertex buffer object for the triangle.</param>
    private static unsafe void CreateVertices(out uint vao, out uint vbo)
    {

        var vertices = new[] {
            -1.0f, -1.0f, 0.0f,
            1.0f, -1.0f, 0.0f,
            0.0f,  1.0f, 0.0f
        };

        vao = GL.glGenVertexArray();
        vbo = GL.glGenBuffer();

        GL.glBindVertexArray(vao);

        GL.glBindBuffer(GL.GL_ARRAY_BUFFER, vbo);
        fixed (float* v = &vertices[0])
        {
            GL.glBufferData(GL.GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL.GL_STATIC_DRAW);
        }

        GL.glVertexAttribPointer(0, 3, GL.GL_FLOAT, false, 3 * sizeof(float), GL.NULL);
        GL.glEnableVertexAttribArray(0);
    }

    private static Random rand;
}
