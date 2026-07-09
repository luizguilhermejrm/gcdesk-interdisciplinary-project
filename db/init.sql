CREATE TABLE IF NOT EXISTS department (
  dep_id INT AUTO_INCREMENT PRIMARY KEY,
  dep_sector VARCHAR(100) NOT NULL,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL
);

INSERT IGNORE INTO department (dep_id, dep_sector) VALUES (1, 'TI');

CREATE TABLE IF NOT EXISTS user (
  user_id INT AUTO_INCREMENT PRIMARY KEY,
  user_name VARCHAR(100),
  user_position VARCHAR(100),
  user_status INT DEFAULT 1,
  user_typeAnalyst VARCHAR(20),
  user_email VARCHAR(100),
  user_password VARCHAR(255),
  user_typeAccess INT DEFAULT 1,
  user_firstLogin INT DEFAULT 0,
  dep_id INT,
  user_image VARCHAR(255),
  last_login VARCHAR(50) DEFAULT NULL,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (dep_id) REFERENCES department(dep_id)
);

INSERT IGNORE INTO user (user_id, user_name, user_position, user_status, user_typeAnalyst, user_email, user_password, user_typeAccess, user_firstLogin, dep_id, user_image) VALUES (1, 'Administrador', 'Analista de Sistemas', 1, 'Analista', 'admin@gcdesk.com', 'f89Lo5HEh4Tt3lmYidbj8eR6J9s27MBQzJLyWb+sOK+tLGihroBNdwdej7ciUD8+yissEAbub2x7dijLRf/9HQ==', 0, 1, 1, NULL);

CREATE TABLE IF NOT EXISTS equipment (
  equip_id INT AUTO_INCREMENT PRIMARY KEY,
  equip_description VARCHAR(255),
  equip_number INT,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS service (
  service_id INT AUTO_INCREMENT PRIMARY KEY,
  service_solution TEXT,
  dep_id INT,
  equip_id INT,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (dep_id) REFERENCES department(dep_id),
  FOREIGN KEY (equip_id) REFERENCES equipment(equip_id)
);

CREATE TABLE IF NOT EXISTS ticket (
  tic_id INT AUTO_INCREMENT PRIMARY KEY,
  tic_description TEXT,
  tic_localization VARCHAR(255),
  tic_type VARCHAR(100),
  tic_priority INT DEFAULT 0,
  tic_status INT DEFAULT 0,
  tic_openTime VARCHAR(50),
  tic_closeTime VARCHAR(50),
  grade INT DEFAULT 0,
  rating INT DEFAULT 0,
  rating_comment TEXT,
  rating_at VARCHAR(50),
  user_id INT,
  ana_analisty_id INT,
  equip_id INT,
  sla_breached INT DEFAULT 0,
  last_response_at VARCHAR(50),
  reopen_count INT DEFAULT 0,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (user_id) REFERENCES user(user_id),
  FOREIGN KEY (ana_analisty_id) REFERENCES user(user_id)
);

CREATE TABLE IF NOT EXISTS user_skill (
  skill_id INT AUTO_INCREMENT PRIMARY KEY,
  user_id INT NOT NULL,
  skill_name VARCHAR(100) NOT NULL,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (user_id) REFERENCES user(user_id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS sla_config (
  sla_id INT AUTO_INCREMENT PRIMARY KEY,
  tic_priority INT NOT NULL,
  response_limit_hours INT NOT NULL,
  resolution_limit_hours INT NOT NULL,
  escalation_hours INT DEFAULT 0,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS notification (
  not_id INT AUTO_INCREMENT PRIMARY KEY,
  not_description TEXT,
  not_title VARCHAR(255),
  tic_id INT,
  not_timeMensage VARCHAR(50),
  not_status INT DEFAULT 0,
  created_at VARCHAR(50) DEFAULT NULL,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (tic_id) REFERENCES ticket(tic_id)
);

CREATE TABLE IF NOT EXISTS audit_log (
  log_id INT AUTO_INCREMENT PRIMARY KEY,
  user_id VARCHAR(50),
  action VARCHAR(100),
  detail TEXT,
  ip_address VARCHAR(45),
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL
);

CREATE TABLE IF NOT EXISTS ticket_history (
  history_id INT AUTO_INCREMENT PRIMARY KEY,
  tic_id INT NOT NULL,
  user_id INT,
  action VARCHAR(50),
  description TEXT,
  created_at VARCHAR(50),
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (tic_id) REFERENCES ticket(tic_id),
  FOREIGN KEY (user_id) REFERENCES user(user_id)
);

CREATE TABLE IF NOT EXISTS ticket_comments (
  comment_id INT AUTO_INCREMENT PRIMARY KEY,
  tic_id INT NOT NULL,
  user_id INT,
  comment_text TEXT NOT NULL,
  created_at VARCHAR(50),
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (tic_id) REFERENCES ticket(tic_id),
  FOREIGN KEY (user_id) REFERENCES user(user_id)
);

CREATE TABLE IF NOT EXISTS password_resets (
  reset_id INT AUTO_INCREMENT PRIMARY KEY,
  user_id INT,
  token VARCHAR(255) NOT NULL,
  expires_at VARCHAR(50),
  used INT DEFAULT 0,
  created_at VARCHAR(50),
  updated_at VARCHAR(50) DEFAULT NULL,
  deleted_at VARCHAR(50) DEFAULT NULL,
  FOREIGN KEY (user_id) REFERENCES user(user_id)
);

-- ============================================================
-- SEED DATA
-- ============================================================

-- Password "123456" hash: ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==

INSERT IGNORE INTO department (dep_id, dep_sector) VALUES
  (2, 'RH'),
  (3, 'Financeiro'),
  (4, 'Administrativo'),
  (5, 'Suporte');

INSERT IGNORE INTO user (user_id, user_name, user_position, user_status, user_typeAnalyst, user_email, user_password, user_typeAccess, user_firstLogin, dep_id, user_image) VALUES
  (2, 'Ana Silva',      'Analista de Suporte',   1, 'Analista',    'ana@gcdesk.com',  'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 0, 1, 1, NULL),
  (3, 'Carlos Santos',  'Analista de Infra',     1, 'Analista',    'carlos@gcdesk.com','ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 0, 1, 1, NULL),
  (4, 'João Oliveira',  'Assistente RH',         1, 'Colaborador', 'joao@gcdesk.com',  'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 1, 1, 2, NULL),
  (5, 'Maria Costa',    'Analista Financeiro',   1, 'Colaborador', 'maria@gcdesk.com', 'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 1, 1, 3, NULL),
  (6, 'Pedro Almeida',  'Aux. Administrativo',   1, 'Colaborador', 'pedro@gcdesk.com', 'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 1, 1, 4, NULL),
  (7, 'Julia Lima',     'Suporte Interno',       1, 'Colaborador', 'julia@gcdesk.com', 'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 1, 1, 5, NULL),
  (8, 'Lucas Ferreira', 'Estagiário TI',         1, 'Colaborador', 'lucas@gcdesk.com', 'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', 1, 1, 1, NULL);

INSERT IGNORE INTO equipment (equip_id, equip_description, equip_number) VALUES
  (1, 'Desktop Dell Optiplex 3070',   15),
  (2, 'Notebook Lenovo ThinkPad T14', 8),
  (3, 'Monitor Samsung 24"',         20),
  (4, 'Impressora HP LaserJet Pro',  3),
  (5, 'Switch Cisco Catalyst 2960',   5),
  (6, 'Roteador MikroTik RB750',     2);

INSERT IGNORE INTO service (service_id, service_solution, dep_id, equip_id) VALUES
  (1, 'Suporte a hardware de desktop e notebooks',     1, 1),
  (2, 'Instalação e configuração de sistemas',         1, 2),
  (3, 'Suporte a redes e conectividade',               1, 5),
  (4, 'Suporte administrativo e sistemas RH',          2, NULL),
  (5, 'Suporte a sistemas financeiros e planilhas',    3, NULL),
  (6, 'Suporte administrativo geral',                  4, NULL),
  (7, 'Atendimento interno e suporte rápido',          5, NULL);

-- Tickets (status: 0=Aberto, 1=Em Andamento, 2=Finalizado)
INSERT IGNORE INTO ticket (tic_id, tic_description, tic_localization, tic_type, tic_priority, tic_status, tic_openTime, tic_closeTime, grade, rating, rating_comment, rating_at, user_id, ana_analisty_id, sla_breached, last_response_at, reopen_count) VALUES
  (1, 'Computador não liga após atualização',         'Sala 201 - RH',      'Manutenção', 0, 0, '06/07/2026 09:15:00', NULL, 0, 0, NULL, NULL, 4, NULL, 0, NULL, 0),
  (2, 'Sistema financeiro muito lento',               'Sala 305 - Financeiro', 'Software',  0, 1, '05/07/2026 14:30:00', NULL, 0, 0, NULL, NULL, 5, 2, 0, NULL, 0),
  (3, 'Impressora não imprime e apresenta erro 404',  'Sala 102 - Adm',     'Manutenção', 0, 2, '04/07/2026 08:00:00', '06/07/2026 11:20:00', 0, 0, NULL, NULL, 6, 3, 0, NULL, 0),
  (4, 'Acesso ao sistema de notas negado',            'Sala 401 - Suporte', 'Registro',   1, 0, '07/07/2026 10:00:00', NULL, 0, 0, NULL, NULL, 7, NULL, 0, NULL, 0),
  (5, 'Solicitação de troca de senha',                'Sala TI',            'Registro',   0, 1, '06/07/2026 16:45:00', NULL, 0, 0, NULL, NULL, 8, 1, 0, NULL, 0),
  (6, 'Configuração de email corporativo no celular', 'RH - Andar 2',       'Redes',      0, 2, '03/07/2026 07:30:00', '05/07/2026 09:00:00', 0, 4, 'Atendimento rápido e eficiente!', '05/07/2026 09:30:00', 4, 2, 0, NULL, 0),
  (7, 'Instalação do pacote Office 365',              'Financeiro - Sala 310', 'Software', 0, 0, '08/07/2026 11:00:00', NULL, 0, 0, NULL, NULL, 5, NULL, 0, NULL, 0),
  (8, 'Monitor com tela piscando',                    'Sala 103 - Adm',     'Manutenção', 0, 1, '07/07/2026 13:20:00', NULL, 0, 0, NULL, NULL, 6, 3, 0, NULL, 0),
  (9, 'Rede wifi instável no andar 3',                'Corredor Financeiro','Redes',      1, 1, '06/07/2026 10:30:00', NULL, 0, 0, NULL, NULL, 5, 2, 0, NULL, 0),
  (10,'Notebook não conecta no projetor',             'Sala Reunião - TI',  'Manutenção', 0, 2, '02/07/2026 15:00:00', '03/07/2026 17:30:00', 0, 5, 'Ótimo atendimento, resolveu rápido.', '03/07/2026 18:00:00', 8, 1, 0, NULL, 0);

-- Notifications
INSERT IGNORE INTO notification (not_id, not_description, not_title, tic_id, not_timeMensage, not_status) VALUES
  (1,  'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      1, '06/07/2026 09:15:00', 0),
  (2,  'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      2, '05/07/2026 14:30:00', 0),
  (3,  'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 2, '05/07/2026 15:00:00', 1),
  (4,  'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      3, '04/07/2026 08:00:00', 0),
  (5,  'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 3, '04/07/2026 08:30:00', 1),
  (6,  'O seu chamado foi finalizado.',                             'Chamado Finalizado',  3, '06/07/2026 11:20:00', 2),
  (7,  'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      4, '07/07/2026 10:00:00', 0),
  (8,  'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      5, '06/07/2026 16:45:00', 0),
  (9,  'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 5, '06/07/2026 17:00:00', 1),
  (10, 'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      6, '03/07/2026 07:30:00', 0),
  (11, 'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 6, '03/07/2026 08:00:00', 1),
  (12, 'O seu chamado foi finalizado.',                             'Chamado Finalizado',  6, '05/07/2026 09:00:00', 2),
  (13, 'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      7, '08/07/2026 11:00:00', 0),
  (14, 'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      8, '07/07/2026 13:20:00', 0),
  (15, 'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 8, '07/07/2026 14:00:00', 1),
  (16, 'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      9, '06/07/2026 10:30:00', 0),
  (17, 'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 9, '06/07/2026 11:00:00', 1),
  (18, 'O seu chamado foi criado com sucesso.',                     'Chamado Criado',      10,'02/07/2026 15:00:00', 0),
  (19, 'O seu chamado foi aceito, está em desenvolvimento.',        'Chamado em Andamento', 10,'02/07/2026 15:30:00', 1),
  (20, 'O seu chamado foi finalizado.',                             'Chamado Finalizado',  10,'03/07/2026 17:30:00', 2);

-- Ticket History
INSERT IGNORE INTO ticket_history (history_id, tic_id, user_id, action, description, created_at) VALUES
  (1,  1, 4, 'CRIADO',     'Chamado criado por João Oliveira',           '06/07/2026 09:15:00'),
  (2,  2, 5, 'CRIADO',     'Chamado criado por Maria Costa',             '05/07/2026 14:30:00'),
  (3,  2, 2, 'ACEITO',     'Chamado aceito por Ana Silva',               '05/07/2026 15:00:00'),
  (4,  3, 6, 'CRIADO',     'Chamado criado por Pedro Almeida',           '04/07/2026 08:00:00'),
  (5,  3, 3, 'ACEITO',     'Chamado aceito por Carlos Santos',           '04/07/2026 08:30:00'),
  (6,  3, 3, 'FINALIZADO', 'Chamado finalizado por Carlos Santos',       '06/07/2026 11:20:00'),
  (7,  4, 7, 'CRIADO',     'Chamado criado por Julia Lima',              '07/07/2026 10:00:00'),
  (8,  5, 8, 'CRIADO',     'Chamado criado por Lucas Ferreira',          '06/07/2026 16:45:00'),
  (9,  5, 1, 'ACEITO',     'Chamado aceito por Administrador',           '06/07/2026 17:00:00'),
  (10, 6, 4, 'CRIADO',     'Chamado criado por João Oliveira',           '03/07/2026 07:30:00'),
  (11, 6, 2, 'ACEITO',     'Chamado aceito por Ana Silva',               '03/07/2026 08:00:00'),
  (12, 6, 2, 'FINALIZADO', 'Chamado finalizado por Ana Silva',           '05/07/2026 09:00:00'),
  (13, 6, 4, 'AVALIACAO',  'Chamado avaliado com nota 4 por João Oliveira', '05/07/2026 09:30:00'),
  (14, 7, 5, 'CRIADO',     'Chamado criado por Maria Costa',             '08/07/2026 11:00:00'),
  (15, 8, 6, 'CRIADO',     'Chamado criado por Pedro Almeida',           '07/07/2026 13:20:00'),
  (16, 8, 3, 'ACEITO',     'Chamado aceito por Carlos Santos',           '07/07/2026 14:00:00'),
  (17, 9, 5, 'CRIADO',     'Chamado criado por Maria Costa',             '06/07/2026 10:30:00'),
  (18, 9, 2, 'ACEITO',     'Chamado aceito por Ana Silva',               '06/07/2026 11:00:00'),
  (19, 10,8, 'CRIADO',     'Chamado criado por Lucas Ferreira',          '02/07/2026 15:00:00'),
  (20, 10,1, 'ACEITO',     'Chamado aceito por Administrador',           '02/07/2026 15:30:00'),
  (21, 10,1, 'FINALIZADO', 'Chamado finalizado por Administrador',       '03/07/2026 17:30:00'),
  (22, 10,8, 'AVALIACAO',  'Chamado avaliado com nota 5 por Lucas Ferreira', '03/07/2026 18:00:00');

-- SLA config (priority 0=normal, 1=alta, 2=urgente)
INSERT IGNORE INTO sla_config (sla_id, tic_priority, response_limit_hours, resolution_limit_hours, escalation_hours) VALUES
  (1, 0, 48, 120, 0),
  (2, 1, 24, 72, 48),
  (3, 2, 4, 24, 8);

-- User Skills (analyst specialization by ticket type)
INSERT IGNORE INTO user_skill (skill_id, user_id, skill_name) VALUES
  (1, 2, 'Software'),
  (2, 2, 'Redes'),
  (3, 3, 'Manutenção'),
  (4, 3, 'Redes'),
  (5, 1, 'Manutenção'),
  (6, 1, 'Software'),
  (7, 1, 'Registro');

-- Ticket Comments
INSERT IGNORE INTO ticket_comments (comment_id, tic_id, user_id, comment_text, created_at) VALUES
  (1, 6, 2, 'João, testei o email e está funcionando normalmente agora. Consegue verificar se chegou alguma mensagem de teste?', '04/07/2026 10:00:00'),
  (2, 6, 4, 'Sim, chegou! Obrigado Ana, funcionou perfeitamente.', '04/07/2026 10:15:00'),
  (3, 3, 3, 'Pedro, verifiquei a impressora. O problema era o cabo USB. Já substituí.', '04/07/2026 09:00:00'),
  (4, 3, 6, 'Obrigado Carlos! Testei agora e está funcionando.', '04/07/2026 09:30:00'),
  (5, 10,1, 'Lucas, testei o notebook no projetor da sala 2 e funcionou. Pode retirar o equipamento.', '03/07/2026 16:00:00'),
  (6, 10,8, 'Perfeito! Obrigado.', '03/07/2026 16:30:00');
