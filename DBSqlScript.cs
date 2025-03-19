namespace PetPals_BackEnd_Group_9
{
    public class DBSqlScript
    {
//        CREATE TABLE roles(
//    role_id INT NOT NULL
//        CONSTRAINT PK_roles PRIMARY KEY IDENTITY,

//    name VARCHAR(20) NOT NULL UNIQUE
//);

//CREATE TABLE users(
//    user_id INT NOT NULL
//        CONSTRAINT PK_users PRIMARY KEY IDENTITY,

//    name VARCHAR(255) NOT NULL,
//    email VARCHAR(100) UNIQUE NOT NULL,
//    password VARCHAR(255) NOT NULL,
//    phone VARCHAR(20),
//    address VARCHAR(255),
//    role_id INT NOT NULL
//        CONSTRAINT FK_users_roles FOREIGN KEY(role_id)
//        REFERENCES roles(role_id),

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);


//CREATE TABLE species(
//    species_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
//    name VARCHAR(100) NOT NULL UNIQUE,
//    description VARCHAR(255) NULL,
//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET()
//);

//CREATE TABLE pets(
//    pet_id INT
//        CONSTRAINT PK_pets PRIMARY KEY IDENTITY,

//    owner_id INT NOT NULL
//        CONSTRAINT FK_pets_users FOREIGN KEY (owner_id)
//        REFERENCES users(user_id)
//        ON DELETE CASCADE,

//    species_id INT NOT NULL
//        CONSTRAINT FK_pets_species FOREIGN KEY (species_id)
//        REFERENCES species(species_id)
//        ON DELETE CASCADE,

//    name VARCHAR(100) NOT NULL,
//    breed VARCHAR(100),
//    age INT,
//    description VARCHAR(255),
//    status VARCHAR(20) NOT NULL DEFAULT 'available'
//        CONSTRAINT CK_pets_status CHECK(status IN ('available', 'adopted')),
//    price DECIMAL(10,2) NOT NULL DEFAULT 0,

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);


//CREATE TABLE adoptions(
//    adoption_id INT
//        CONSTRAINT PK_adoptions PRIMARY KEY IDENTITY,

//    adopter_id INT NOT NULL
//        CONSTRAINT FK_adoptions_users FOREIGN KEY (adopter_id)
//        REFERENCES users(user_id)
//        ON DELETE NO ACTION,

//    pet_id INT NOT NULL
//        CONSTRAINT FK_adoptions_pets FOREIGN KEY (pet_id)
//        REFERENCES pets(pet_id)
//        ON DELETE NO ACTION,

//    adoption_date DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    status VARCHAR(20) NOT NULL DEFAULT 'pending'
//        CONSTRAINT CK_adoptions_status CHECK(status IN ('pending', 'approved', 'rejected')),

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);


//CREATE TABLE service_categories(
//    category_id INT NOT NULL
//        CONSTRAINT PK_service_categories PRIMARY KEY IDENTITY,

//    name VARCHAR(50) NOT NULL UNIQUE
//);


//CREATE TABLE services(
//    service_id INT
//        CONSTRAINT PK_services PRIMARY KEY IDENTITY,

//    provider_id INT NOT NULL
//        CONSTRAINT FK_services_users FOREIGN KEY (provider_id)
//        REFERENCES users(user_id)
//        ON DELETE CASCADE,

//    name VARCHAR(100) NOT NULL,
//    category_id INT NOT NULL
//        CONSTRAINT FK_services_categories FOREIGN KEY(category_id)
//        REFERENCES service_categories(category_id),

//    description VARCHAR(255),
//    price DECIMAL(10,2) NOT NULL,

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);

//CREATE TABLE forum_posts(
//    forum_post_id INT
//        CONSTRAINT PK_forum_posts PRIMARY KEY IDENTITY,

//    user_id INT NOT NULL
//        FOREIGN KEY (user_id)
//        REFERENCES users(user_id)
//        ON DELETE CASCADE,

//    title VARCHAR(255) NOT NULL,
//    content TEXT NOT NULL,

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);

//CREATE TABLE forum_comments(
//    forum_comment_id INT
//        CONSTRAINT PK_forum_comments PRIMARY KEY IDENTITY,

//    post_id INT NOT NULL
//        CONSTRAINT FK_forum_comments_posts FOREIGN KEY (post_id)
//        REFERENCES forum_posts(forum_post_id)
//        ON DELETE CASCADE,

//    user_id INT NOT NULL
//        CONSTRAINT FK_forum_comments_users FOREIGN KEY (user_id)
//        REFERENCES users(user_id)
//        ON DELETE NO ACTION,

//    comment VARCHAR(255) NOT NULL,

//    created_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    created_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM',
//    updated_at DATETIMEOFFSET NOT NULL DEFAULT SYSDATETIMEOFFSET(),
//    updated_by VARCHAR(255) NOT NULL DEFAULT 'SYSTEM'
//);

//INSERT INTO roles(name)
//VALUES
//    ('Adopter'),
//	('Owner'),
//	('Provider')

//DECLARE @salt NVARCHAR(50) = 'random_salt_123'; -- Bisa di-generate lebih random
//DECLARE @password NVARCHAR(100) = 'password123';

//INSERT INTO users(name, email, password, phone, address, role_id)
//VALUES
//    ('Asep', 'asep123@gmail.com', HASHBYTES('SHA2_512', @salt + @password), '089912345678', 'Jalan Jeruk', 1),
//	('Budi', 'budi123@gmail.com', HASHBYTES('SHA2_512', @salt + @password), '089912345678', 'Jalan Semangka', 2),
//	('Siti', 'siti123@gmail.com', HASHBYTES('SHA2_512', @salt + @password), '089912345678', 'Jalan Apel', 2),
//	('Cecep', 'cecep123@gmail.com', HASHBYTES('SHA2_512', @salt + @password), '089912345678', 'Jalan Belimbing', 3)

//SELECT* FROM species;

//INSERT INTO species(name, description)
//VALUES
//    ('Dog', 'A domesticated mammal of the family Canidae.'),
//	('Cat', 'A small, typically furry, omnivorous mammal.')

//INSERT INTO pets(owner_id, species_id, name, breed, age, description, status, price)
//VALUES
//    (2, 1, 'Bella', 'Golden Retriever', 3, 'lorem ipsum dolor', 'adopted', 1000000),
//	(2, 1, 'Axel', 'Pitbull', 2, 'lorem ipsum dolor', 'available', 1100000),
//	(3, 1, 'Xavier', 'Chihuahua', 3, 'lorem ipsum dolor', 'available', 1200000),	
//	(3, 2, 'Stella', 'British Shorthair', 3, 'Gray fur cat', 'available', 1300000)


    }
}
