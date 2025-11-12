using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using patrimonio_digital.MVVM.Model;

public class AuthService
{
    private List<Usuario> _users = new();

    public AuthService()
    {
        // Opcional: carregar usuários do JSON ao iniciar
        LoadUsersFromJson("usuarios.json");
    }

    #region Cadastro de Usuário

    public bool Register(string nome, string senha, TipoUsuario role = TipoUsuario.Visitante)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            return false;

        if (_users.Any(u => u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            return false; // usuário já existe

        var usuario = new Usuario
        {
            Nome = nome.Trim(),
            Senha = HashPassword(senha.Trim()),
            Tipo = role
        };

        _users.Add(usuario);

        SaveUsersToJson("usuarios.json"); // salva imediatamente

        return true;
    }

    #endregion

    #region Login

    public Usuario? Login(string nome, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            return null;

        nome = nome.Trim();
        senha = senha.Trim();

        var usuario = _users.FirstOrDefault(u =>
            u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        if (usuario == null)
            return null;

        return VerifyPassword(senha, usuario.Senha) ? usuario : null;
    }

    #endregion

    #region Hash de Senha

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    #endregion

    #region JSON com System.Text.Json

    public void SaveUsersToJson(string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() } // serializa enum como texto
        };
        var json = JsonSerializer.Serialize(_users, options);
        File.WriteAllText(path, json);
    }

    public void LoadUsersFromJson(string path)
    {
        if (!File.Exists(path))
            return;

        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() } // desserializa enum como texto
        };
        _users = JsonSerializer.Deserialize<List<Usuario>>(json, options) ?? new List<Usuario>();
    }

    #endregion
}
