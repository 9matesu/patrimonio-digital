using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using patrimonio_digital.MVVM.Model;

public class AuthService
{
    private List<Usuario> _users = new();
    private const string UsersFilePath = "usuarios.json";

    public Usuario? UsuarioLogado { get; private set; }

    public AuthService()
    {
        LoadUsersFromJson(UsersFilePath);

        // Cria usuário admin padrão se lista estiver vazia
        if (!_users.Any())
        {
            Register("admin", "admin", TipoUsuario.Administrador);
        }
    }

    public bool Register(string nome, string senha, TipoUsuario tipo = TipoUsuario.Visitante)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            return false;

        if (_users.Any(u => u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            return false; // já existe

        var usuario = new Usuario
        {
            Nome = nome.Trim(),
            Senha = HashPassword(senha.Trim()),
            Tipo = tipo
        };

        _users.Add(usuario);
        SaveUsersToJson(UsersFilePath);
        return true;
    }

    public Usuario? Login(string nome, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            return null;

        var usuario = _users.FirstOrDefault(u => u.Nome.Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase));
        if (usuario == null) return null;

        if (VerifyPassword(senha, usuario.Senha))
        {
            UsuarioLogado = usuario;
            return usuario;
        }
        return null;
    }

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

    public void SaveUsersToJson(string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        var json = JsonSerializer.Serialize(_users, options);
        File.WriteAllText(path, json);
    }

    public void LoadUsersFromJson(string path)
    {
        if (!File.Exists(path))
        {
            _users = new List<Usuario>();
            return;
        }
        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        _users = JsonSerializer.Deserialize<List<Usuario>>(json, options) ?? new List<Usuario>();
    }

    public List<Usuario> GetAllUsers()
    {
        return new List<Usuario>(_users);
    }

    public bool RemoveUser(Usuario usuario)
    {
        if (_users.Remove(usuario))
        {
            SaveUsersToJson(UsersFilePath);
            return true;
        }
        return false;
    }

    public bool AtualizarUsuario(Usuario usuarioAntigo, string novoNome, string novaSenha, TipoUsuario novoTipo)
    {
        if (usuarioAntigo == null)
            return false;

        // Verifica se já existe outro usuário com o novo nome
        if (!_users
            .Where(u => u != usuarioAntigo)
            .Any(u => u.Nome.Equals(novoNome, StringComparison.OrdinalIgnoreCase)))
        {
            usuarioAntigo.Nome = novoNome;
            usuarioAntigo.Tipo = novoTipo;

            if (!string.IsNullOrEmpty(novaSenha))
            {
                usuarioAntigo.Senha = HashPassword(novaSenha);
            }

            SaveUsersToJson(UsersFilePath);
            return true;
        }
        return false; // nome duplicado
    }
}
