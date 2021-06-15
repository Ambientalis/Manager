
    
alter table abastecimentos  drop foreign key FK83C6C02041EAE73E


    
alter table arquivos  drop foreign key FK24FC38A48D9E7799


    
alter table arquivos  drop foreign key FK24FC38A42AF80D


    
alter table arquivos  drop foreign key FK24FC38A442CBD094


    
alter table arquivos  drop foreign key FK24FC38A45BEAFD21


    
alter table atividades  drop foreign key FK58D0ACFF18795A4A


    
alter table atividades  drop foreign key FK58D0ACFF96AEC13A


    
alter table atividades  drop foreign key FK58D0ACFF42CBD094


    
alter table classificacoes_clientes  drop foreign key FK95F65D537DE120B3


    
alter table contatos  drop foreign key FKE0EDA9CB7DE120B3


    
alter table controles_viagens  drop foreign key FK10ACA9B84C206C0B


    
alter table controles_viagens  drop foreign key FK10ACA9B86F7EED1C


    
alter table controles_viagens  drop foreign key FK10ACA9B86562F10D


    
alter table controles_viagens  drop foreign key FK10ACA9B8E05BDA5F


    
alter table detalhamentos  drop foreign key FKAD9A9B628D9E7799


    
alter table detalhamentos  drop foreign key FKAD9A9B622AF80D


    
alter table detalhamentos  drop foreign key FKAD9A9B6242CBD094


    
alter table detalhamentos  drop foreign key FKAD9A9B6249DEF829


    
alter table detalhamentos  drop foreign key FKAD9A9B625BEAFD21


    
alter table funcoes  drop foreign key FK26967B94D8008DF0


    
alter table funcoes  drop foreign key FK26967B946C1AEDB0


    
alter table funcionarios_funcoes  drop foreign key FKAF9A291D2A417FF7


    
alter table funcionarios_funcoes  drop foreign key FKAF9A291DFC9EFD69


    
alter table menus  drop foreign key FK15074AC08A168557


    
alter table ocorrencias  drop foreign key FK8EA8E371129CBBF4


    
alter table ocorrencias  drop foreign key FK8EA8E371A0DF21BD


    
alter table pedidos  drop foreign key FKC41E8E9B7DE120B3


    
alter table pedidos  drop foreign key FKC41E8E9B76344901


    
alter table pedidos  drop foreign key FKC41E8E9B7C32AE53


    
alter table permissoes  drop foreign key FKFCDACAACFC9EFD69


    
alter table permissoes  drop foreign key FKFCDACAAC2A417FF7


    
alter table telas_permissoes  drop foreign key FK80FAE0E1CBB469C


    
alter table telas_permissoes  drop foreign key FK80FAE0E19AF5C0D5


    
alter table preferencias_relatorio  drop foreign key FK981DCA242A417FF7


    
alter table preferencias_relatorio  drop foreign key FK981DCA24CBB469C


    
alter table reservas  drop foreign key FKF71D0A106562F10D


    
alter table reservas  drop foreign key FKF71D0A105BEAFD21


    
alter table reservas  drop foreign key FKF71D0A10F0377FF1


    
alter table reservas  drop foreign key FKF71D0A104C206C0B


    
alter table setores  drop foreign key FKCF94A1BA20279055


    
alter table solicitacoes_adiamentos  drop foreign key FK85F23DFD42CBD094


    
alter table solicitacoes_aprovacoes  drop foreign key FK8000E3C642CBD094


    
alter table telas  drop foreign key FK46FC62719CC57A9C


    
alter table ordens_servico  drop foreign key FK933E12472AF80D


    
alter table ordens_servico  drop foreign key FK933E12474C206C0B


    
alter table ordens_servico  drop foreign key FK933E12476C1AEDB0


    
alter table ordens_servico  drop foreign key FK933E1247FB4746A0


    
alter table ordens_servico  drop foreign key FK933E124770EA4004


    
alter table ordens_servico  drop foreign key FK933E1247D0D68DDF


    
alter table ordens_servico_funcionarios  drop foreign key FKD57DB35A2A417FF7


    
alter table ordens_servico_funcionarios  drop foreign key FKD57DB35A42CBD094


    
alter table cidades  drop foreign key FK5408C30D8E8E305


    
alter table pessoas  drop foreign key FK6762AA3F783F1D6D


    
alter table veiculos_departamentos  drop foreign key FK87E3B95F6562F10D


    
alter table veiculos_departamentos  drop foreign key FK87E3B95F20279055


    
alter table enderecos  drop foreign key FK8AB8F2C6DECCE159


    
alter table enderecos  drop foreign key FK8AB8F2C6C087113D


    
alter table veiculos  drop foreign key FKB0F6304936515311


    
alter table veiculos  drop foreign key FKB0F63049410DA6F0


    
alter table visitas  drop foreign key FK9890A9102DBCAA4F


    
alter table visitas  drop foreign key FK9890A910BD057632


    
alter table visitas  drop foreign key FK9890A91042CBD094


    
alter table funcionarios  drop foreign key FK1DC35AD37D120B9A


    
alter table clientes  drop foreign key FKFC4BBD197D120B9A


    drop table if exists abastecimentos

    drop table if exists arquivos

    drop table if exists atividades

    drop table if exists cargos

    drop table if exists classificacoes_clientes

    drop table if exists configuracoes

    drop table if exists contatos

    drop table if exists controles_viagens

    drop table if exists detalhamentos

    drop table if exists formas_de_pagamento

    drop table if exists funcoes

    drop table if exists funcionarios_funcoes

    drop table if exists menus

    drop table if exists ocorrencias

    drop table if exists orgaos

    drop table if exists pedidos

    drop table if exists permissoes

    drop table if exists telas_permissoes

    drop table if exists preferencias_relatorio

    drop table if exists reservas

    drop table if exists setores

    drop table if exists solicitacoes_adiamentos

    drop table if exists solicitacoes_aprovacoes

    drop table if exists telas

    drop table if exists ordens_servico

    drop table if exists ordens_servico_funcionarios

    drop table if exists tipos_atividades

    drop table if exists tipos_despesas

    drop table if exists tipos_ocorrencias_veiculos

    drop table if exists tipos_ordem_servico

    drop table if exists cidades

    drop table if exists pessoas

    drop table if exists departamentos

    drop table if exists veiculos_departamentos

    drop table if exists imagens

    drop table if exists enderecos

    drop table if exists estados

    drop table if exists tipos_pedidos

    drop table if exists tipos_reservas_veiculos

    drop table if exists tipos_visitas

    drop table if exists unidades

    drop table if exists veiculos

    drop table if exists visitas

    drop table if exists funcionarios

    drop table if exists clientes

    create table abastecimentos (
        id INTEGER not null,
       version INTEGER not null,
       data DATETIME,
       qtd_litros NUMERIC(19,5),
       quilometragem_geral NUMERIC(19,5),
       valor_unitario NUMERIC(19,5),
       valor_total NUMERIC(19,5),
       Ativo CHAR(1),
       Emp INTEGER,
       id_controle_viagem INTEGER,
       primary key (id)
    )

    create table arquivos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       host VARCHAR(255),
       caminho VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_atividade INTEGER,
       id_pedido INTEGER,
       id_ordem_servico INTEGER,
       id_visita INTEGER,
       primary key (id)
    )

    create table atividades (
        id INTEGER not null,
       version INTEGER not null,
       data DATETIME,
       descricao text,
       Ativo CHAR(1),
       Emp INTEGER,
       id_executor INTEGER,
       id_tipo_atividade INTEGER,
       id_ordem_servico INTEGER,
       primary key (id)
    )

    create table cargos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table classificacoes_clientes (
        id INTEGER not null,
       version INTEGER not null,
       classificacao INTEGER,
       data DATETIME,
       Ativo CHAR(1),
       Emp INTEGER,
       id_cliente INTEGER,
       primary key (id)
    )

    create table configuracoes (
        id INTEGER not null,
       version INTEGER not null,
       emails_aviso_contratacao_aceita VARCHAR(255),
       emails_aviso_orcamento_aceito VARCHAR(255),
       emails_aviso_pesquisa_satisfacao_respondida VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table contatos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       funcao VARCHAR(255),
       telefone1 VARCHAR(255),
       telefone2 VARCHAR(255),
       email VARCHAR(255),
       recebe_notificacoes CHAR(1),
       email_recebe_login_senha CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_cliente INTEGER,
       primary key (id)
    )

    create table controles_viagens (
        id INTEGER not null,
       version INTEGER not null,
       data DATETIME,
       data_hora_saida DATETIME,
       data_hora_chegada DATETIME,
       quilometragem_saida NUMERIC(19,5),
       quilometragem_chegada NUMERIC(19,5),
       roteiro VARCHAR(255),
       observacoes text,
       Ativo CHAR(1),
       Emp INTEGER,
       id_responsavel INTEGER,
       id_motorista INTEGER,
       id_veiculo INTEGER,
       id_setor_utilizacao INTEGER,
       primary key (id)
    )

    create table detalhamentos (
        id INTEGER not null,
       version INTEGER not null,
       usuario VARCHAR(255),
       data_salvamento DATETIME,
       conteudo text,
       Ativo CHAR(1),
       Emp INTEGER,
       id_atividade INTEGER,
       id_pedido INTEGER,
       id_ordem_servico INTEGER,
       id_ordem_servico_observacao INTEGER,
       id_visita INTEGER,
       primary key (id)
    )

    create table formas_de_pagamento (
        id INTEGER not null,
       version INTEGER not null,
       prazo_primeiro_pagamento INTEGER,
       qtd_vezes INTEGER,
       acrescimo_desconto NUMERIC(19,5),
       tipo VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table funcoes (
        id INTEGER not null,
       version INTEGER not null,
       Ativo CHAR(1),
       Emp INTEGER,
       id_cargo INTEGER,
       id_setor INTEGER,
       primary key (id)
    )

    create table funcionarios_funcoes (
        id_funcao INTEGER not null,
       id_funcionario INTEGER not null
    )

    create table menus (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_menu_pai INTEGER,
       primary key (id)
    )

    create table ocorrencias (
        id INTEGER not null,
       version INTEGER not null,
       descricao text,
       data DATETIME,
       Ativo CHAR(1),
       Emp INTEGER,
       id_tipo_ocorrencia_veiculo INTEGER,
       id_reserva INTEGER,
       primary key (id)
    )

    create table orgaos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table pedidos (
        id INTEGER not null,
       version INTEGER not null,
       codigo VARCHAR(255),
       data DATETIME,
       unidade CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_cliente INTEGER,
       id_tipo_pedido INTEGER,
       id_vendedor INTEGER,
       primary key (id)
    )

    create table permissoes (
        id INTEGER not null,
       version INTEGER not null,
       acessa_os CHAR(1),
       aprova_os CHAR(1),
       adia_prazo_legal_os CHAR(1),
       adia_prazo_diretoria_os CHAR(1),
       visualiza_controle_viagens CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_funcao INTEGER,
       id_funcionario INTEGER,
       primary key (id)
    )

    create table telas_permissoes (
        id_permissao INTEGER not null,
       id_tela INTEGER not null
    )

    create table preferencias_relatorio (
        id INTEGER not null,
       version INTEGER not null,
       preferencia VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_funcionario INTEGER,
       id_tela INTEGER,
       primary key (id)
    )

    create table reservas (
        id INTEGER not null,
       version INTEGER not null,
       data_inicio DATETIME,
       data_fim DATETIME,
       descricao text,
       quilometragem NUMERIC(19,5),
       consumo NUMERIC(19,5),
       status CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_veiculo INTEGER,
       id_visita INTEGER,
       id_tipo_reserva_veiculo INTEGER,
       id_responsavel INTEGER,
       primary key (id)
    )

    create table setores (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_departamento INTEGER,
       primary key (id)
    )

    create table solicitacoes_adiamentos (
        id INTEGER not null,
       version INTEGER not null,
       data DATETIME,
       solicitante VARCHAR(255),
       justificativa text,
       parecer CHAR(1),
       prazo_padrao_anterior DATETIME,
       prazo_legal_anterior DATETIME,
       prazo_diretor_anterior DATETIME,
       prazo_padrao_novo DATETIME,
       prazo_legal_novo DATETIME,
       prazo_diretoria_novo DATETIME,
       usuario_adiou VARCHAR(255),
       observacoes text,
       data_resposta DATETIME,
       Ativo CHAR(1),
       Emp INTEGER,
       id_ordem_servico INTEGER,
       primary key (id)
    )

    create table solicitacoes_aprovacoes (
        id INTEGER not null,
       version INTEGER not null,
       data DATETIME,
       justificativa text,
       solicitante VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_ordem_servico INTEGER,
       primary key (id)
    )

    create table telas (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       url VARCHAR(255),
       tooltip text,
       exibir_menu CHAR(1),
       relatorio CHAR(1),
       prioridade INTEGER,
       relatorio_grafico CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_menu INTEGER,
       primary key (id)
    )

    create table ordens_servico (
        id INTEGER not null,
       version INTEGER not null,
       codigo VARCHAR(255),
       sem_custo CHAR(1),
       data DATETIME,
       descricao text,
       prazo_padrao DATETIME,
       prazo_legal DATETIME,
       prazo_diretoria DATETIME,
       numero_processo_orgao VARCHAR(255),
       renovavel CHAR(1),
       prazo_renovacao INTEGER,
       tipo_renovacao CHAR(1),
       ja_renovada CHAR(1),
       data_encerramento DATETIME,
       protocolo_oficio_encerramento VARCHAR(255),
       data_protocolo_encerramento DATETIME,
       status CHAR(1),
       justificativa_aprovacao text,
       data_aprovacao DATETIME,
       usuario_aprovou VARCHAR(255),
       possui_protocolo CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       id_pedido INTEGER,
       id_responsavel INTEGER,
       id_setor INTEGER,
       id_orgao INTEGER,
       id_tipo_ordem_servico INTEGER,
       id_ordem_matriz INTEGER,
       primary key (id)
    )

    create table ordens_servico_funcionarios (
        id_ordem_servico INTEGER not null,
       id_funcionario INTEGER not null
    )

    create table tipos_atividades (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_despesas (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       pre_aprovada CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_ocorrencias_veiculos (
        id INTEGER not null,
       version INTEGER not null,
       descricao VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_ordem_servico (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       prazo_padrao INTEGER,
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table cidades (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_estado INTEGER,
       primary key (id)
    )

    create table pessoas (
        id INTEGER not null,
       version INTEGER not null,
       codigo VARCHAR(18),
       tipo CHAR(1),
       nome_razao_social VARCHAR(255),
       apelido_nome_fantasia VARCHAR(255),
       cpf_cnpj VARCHAR(18),
       inscricao_estadual VARCHAR(80),
       IsentoICMS CHAR(1),
       inscricao_municipal VARCHAR(80),
       rg VARCHAR(80),
       emissor_rg VARCHAR(80),
       Nacionalidade VARCHAR(30),
       data_nascimento DATETIME,
       estado_civil VARCHAR(30),
       sexo CHAR(1),
       observacoes text,
       Ativo CHAR(1),
       Emp INTEGER,
       id_imagem INTEGER,
       primary key (id)
    )

    create table departamentos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table veiculos_departamentos (
        id_departamento INTEGER not null,
       id_veiculo INTEGER not null
    )

    create table imagens (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       host VARCHAR(255),
       caminho VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table enderecos (
        id INTEGER not null,
       version INTEGER not null,
       descricao VARCHAR(255),
       logradouro VARCHAR(255),
       numero VARCHAR(8),
       bairro VARCHAR(255),
       referencia VARCHAR(255),
       complemento VARCHAR(100),
       cep VARCHAR(10),
       Ativo CHAR(1),
       Emp INTEGER,
       id_cidade INTEGER,
       id_pessoa INTEGER,
       primary key (id)
    )

    create table estados (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       sigla VARCHAR(2),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_pedidos (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_reservas_veiculos (
        id INTEGER not null,
       version INTEGER not null,
       descricao VARCHAR(255),
       tipo_visita_os CHAR(1),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table tipos_visitas (
        id INTEGER not null,
       version INTEGER not null,
       nome VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table unidades (
        id INTEGER not null,
       version INTEGER not null,
       codigo VARCHAR(255),
       descricao VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       primary key (id)
    )

    create table veiculos (
        id INTEGER not null,
       version INTEGER not null,
       placa VARCHAR(255),
       descricao VARCHAR(255),
       Ativo CHAR(1),
       Emp INTEGER,
       id_gestor INTEGER,
       id_departamento_responsavel INTEGER,
       primary key (id)
    )

    create table visitas (
        id INTEGER not null,
       version INTEGER not null,
       data_inicio DATETIME,
       data_fim DATETIME,
       descricao text,
       aceita CHAR(1),
       email_aceitou VARCHAR(255),
       motivo_nao_aceite text,
       Ativo CHAR(1),
       Emp INTEGER,
       id_visitante INTEGER,
       id_tipo_visita INTEGER,
       id_ordem_servico INTEGER,
       primary key (id)
    )

    create table funcionarios (
        id INTEGER not null,
       celular_corporativo VARCHAR(255),
       celular_pessoal VARCHAR(255),
       telefone_residencial VARCHAR(255),
       nome_contato_emergencia VARCHAR(255),
       telefone_contato_emergencia VARCHAR(255),
       login VARCHAR(255),
       senha VARCHAR(255),
       email_corporativo VARCHAR(255),
       email_pessoal VARCHAR(255),
       vendedor CHAR(1),
       unidade CHAR(1),
       primary key (id)
    )

    create table clientes (
        id INTEGER not null,
       eh_cliente CHAR(1),
       eh_fornecedor CHAR(1),
       telefone1 VARCHAR(255),
       telefone2 VARCHAR(255),
       email VARCHAR(255),
       email_recebe_notificacoes CHAR(1),
       email_recebe_login_senha CHAR(1),
       login VARCHAR(255),
       senha VARCHAR(255),
       exibir_site CHAR(1),
       unidade CHAR(1),
       primary key (id)
    )

    alter table abastecimentos 
        add index (id_controle_viagem), 
        add constraint FK83C6C02041EAE73E 
        foreign key (id_controle_viagem) 
        references controles_viagens (id)

    alter table arquivos 
        add index (id_atividade), 
        add constraint FK24FC38A48D9E7799 
        foreign key (id_atividade) 
        references atividades (id)

    alter table arquivos 
        add index (id_pedido), 
        add constraint FK24FC38A42AF80D 
        foreign key (id_pedido) 
        references pedidos (id)

    alter table arquivos 
        add index (id_ordem_servico), 
        add constraint FK24FC38A442CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table arquivos 
        add index (id_visita), 
        add constraint FK24FC38A45BEAFD21 
        foreign key (id_visita) 
        references visitas (id)

    alter table atividades 
        add index (id_executor), 
        add constraint FK58D0ACFF18795A4A 
        foreign key (id_executor) 
        references funcionarios (id)

    alter table atividades 
        add index (id_tipo_atividade), 
        add constraint FK58D0ACFF96AEC13A 
        foreign key (id_tipo_atividade) 
        references tipos_atividades (id)

    alter table atividades 
        add index (id_ordem_servico), 
        add constraint FK58D0ACFF42CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table classificacoes_clientes 
        add index (id_cliente), 
        add constraint FK95F65D537DE120B3 
        foreign key (id_cliente) 
        references clientes (id)

    alter table contatos 
        add index (id_cliente), 
        add constraint FKE0EDA9CB7DE120B3 
        foreign key (id_cliente) 
        references clientes (id)

    alter table controles_viagens 
        add index (id_responsavel), 
        add constraint FK10ACA9B84C206C0B 
        foreign key (id_responsavel) 
        references funcionarios (id)

    alter table controles_viagens 
        add index (id_motorista), 
        add constraint FK10ACA9B86F7EED1C 
        foreign key (id_motorista) 
        references funcionarios (id)

    alter table controles_viagens 
        add index (id_veiculo), 
        add constraint FK10ACA9B86562F10D 
        foreign key (id_veiculo) 
        references veiculos (id)

    alter table controles_viagens 
        add index (id_setor_utilizacao), 
        add constraint FK10ACA9B8E05BDA5F 
        foreign key (id_setor_utilizacao) 
        references setores (id)

    alter table detalhamentos 
        add index (id_atividade), 
        add constraint FKAD9A9B628D9E7799 
        foreign key (id_atividade) 
        references atividades (id)

    alter table detalhamentos 
        add index (id_pedido), 
        add constraint FKAD9A9B622AF80D 
        foreign key (id_pedido) 
        references pedidos (id)

    alter table detalhamentos 
        add index (id_ordem_servico), 
        add constraint FKAD9A9B6242CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table detalhamentos 
        add index (id_ordem_servico_observacao), 
        add constraint FKAD9A9B6249DEF829 
        foreign key (id_ordem_servico_observacao) 
        references ordens_servico (id)

    alter table detalhamentos 
        add index (id_visita), 
        add constraint FKAD9A9B625BEAFD21 
        foreign key (id_visita) 
        references visitas (id)

    alter table funcoes 
        add index (id_cargo), 
        add constraint FK26967B94D8008DF0 
        foreign key (id_cargo) 
        references cargos (id)

    alter table funcoes 
        add index (id_setor), 
        add constraint FK26967B946C1AEDB0 
        foreign key (id_setor) 
        references setores (id)

    alter table funcionarios_funcoes 
        add index (id_funcionario), 
        add constraint FKAF9A291D2A417FF7 
        foreign key (id_funcionario) 
        references funcionarios (id)

    alter table funcionarios_funcoes 
        add index (id_funcao), 
        add constraint FKAF9A291DFC9EFD69 
        foreign key (id_funcao) 
        references funcoes (id)

    alter table menus 
        add index (id_menu_pai), 
        add constraint FK15074AC08A168557 
        foreign key (id_menu_pai) 
        references menus (id)

    alter table ocorrencias 
        add index (id_tipo_ocorrencia_veiculo), 
        add constraint FK8EA8E371129CBBF4 
        foreign key (id_tipo_ocorrencia_veiculo) 
        references tipos_ocorrencias_veiculos (id)

    alter table ocorrencias 
        add index (id_reserva), 
        add constraint FK8EA8E371A0DF21BD 
        foreign key (id_reserva) 
        references reservas (id)

    alter table pedidos 
        add index (id_cliente), 
        add constraint FKC41E8E9B7DE120B3 
        foreign key (id_cliente) 
        references clientes (id)

    alter table pedidos 
        add index (id_tipo_pedido), 
        add constraint FKC41E8E9B76344901 
        foreign key (id_tipo_pedido) 
        references tipos_pedidos (id)

    alter table pedidos 
        add index (id_vendedor), 
        add constraint FKC41E8E9B7C32AE53 
        foreign key (id_vendedor) 
        references funcionarios (id)

    alter table permissoes 
        add index (id_funcao), 
        add constraint FKFCDACAACFC9EFD69 
        foreign key (id_funcao) 
        references funcoes (id)

    alter table permissoes 
        add index (id_funcionario), 
        add constraint FKFCDACAAC2A417FF7 
        foreign key (id_funcionario) 
        references funcionarios (id)

    alter table telas_permissoes 
        add index (id_tela), 
        add constraint FK80FAE0E1CBB469C 
        foreign key (id_tela) 
        references telas (id)

    alter table telas_permissoes 
        add index (id_permissao), 
        add constraint FK80FAE0E19AF5C0D5 
        foreign key (id_permissao) 
        references permissoes (id)

    alter table preferencias_relatorio 
        add index (id_funcionario), 
        add constraint FK981DCA242A417FF7 
        foreign key (id_funcionario) 
        references funcionarios (id)

    alter table preferencias_relatorio 
        add index (id_tela), 
        add constraint FK981DCA24CBB469C 
        foreign key (id_tela) 
        references telas (id)

    alter table reservas 
        add index (id_veiculo), 
        add constraint FKF71D0A106562F10D 
        foreign key (id_veiculo) 
        references veiculos (id)

    alter table reservas 
        add index (id_visita), 
        add constraint FKF71D0A105BEAFD21 
        foreign key (id_visita) 
        references visitas (id)

    alter table reservas 
        add index (id_tipo_reserva_veiculo), 
        add constraint FKF71D0A10F0377FF1 
        foreign key (id_tipo_reserva_veiculo) 
        references tipos_reservas_veiculos (id)

    alter table reservas 
        add index (id_responsavel), 
        add constraint FKF71D0A104C206C0B 
        foreign key (id_responsavel) 
        references funcionarios (id)

    alter table setores 
        add index (id_departamento), 
        add constraint FKCF94A1BA20279055 
        foreign key (id_departamento) 
        references departamentos (id)

    alter table solicitacoes_adiamentos 
        add index (id_ordem_servico), 
        add constraint FK85F23DFD42CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table solicitacoes_aprovacoes 
        add index (id_ordem_servico), 
        add constraint FK8000E3C642CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table telas 
        add index (id_menu), 
        add constraint FK46FC62719CC57A9C 
        foreign key (id_menu) 
        references menus (id)

    alter table ordens_servico 
        add index (id_pedido), 
        add constraint FK933E12472AF80D 
        foreign key (id_pedido) 
        references pedidos (id)

    alter table ordens_servico 
        add index (id_responsavel), 
        add constraint FK933E12474C206C0B 
        foreign key (id_responsavel) 
        references funcionarios (id)

    alter table ordens_servico 
        add index (id_setor), 
        add constraint FK933E12476C1AEDB0 
        foreign key (id_setor) 
        references setores (id)

    alter table ordens_servico 
        add index (id_orgao), 
        add constraint FK933E1247FB4746A0 
        foreign key (id_orgao) 
        references orgaos (id)

    alter table ordens_servico 
        add index (id_tipo_ordem_servico), 
        add constraint FK933E124770EA4004 
        foreign key (id_tipo_ordem_servico) 
        references tipos_ordem_servico (id)

    alter table ordens_servico 
        add index (id_ordem_matriz), 
        add constraint FK933E1247D0D68DDF 
        foreign key (id_ordem_matriz) 
        references ordens_servico (id)

    alter table ordens_servico_funcionarios 
        add index (id_funcionario), 
        add constraint FKD57DB35A2A417FF7 
        foreign key (id_funcionario) 
        references funcionarios (id)

    alter table ordens_servico_funcionarios 
        add index (id_ordem_servico), 
        add constraint FKD57DB35A42CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table cidades 
        add index (id_estado), 
        add constraint FK5408C30D8E8E305 
        foreign key (id_estado) 
        references estados (id)

    alter table pessoas 
        add index (id_imagem), 
        add constraint FK6762AA3F783F1D6D 
        foreign key (id_imagem) 
        references imagens (id)

    alter table veiculos_departamentos 
        add index (id_veiculo), 
        add constraint FK87E3B95F6562F10D 
        foreign key (id_veiculo) 
        references veiculos (id)

    alter table veiculos_departamentos 
        add index (id_departamento), 
        add constraint FK87E3B95F20279055 
        foreign key (id_departamento) 
        references departamentos (id)

    alter table enderecos 
        add index (id_cidade), 
        add constraint FK8AB8F2C6DECCE159 
        foreign key (id_cidade) 
        references cidades (id)

    alter table enderecos 
        add index (id_pessoa), 
        add constraint FK8AB8F2C6C087113D 
        foreign key (id_pessoa) 
        references pessoas (id)

    alter table veiculos 
        add index (id_gestor), 
        add constraint FKB0F6304936515311 
        foreign key (id_gestor) 
        references funcionarios (id)

    alter table veiculos 
        add index (id_departamento_responsavel), 
        add constraint FKB0F63049410DA6F0 
        foreign key (id_departamento_responsavel) 
        references departamentos (id)

    alter table visitas 
        add index (id_visitante), 
        add constraint FK9890A9102DBCAA4F 
        foreign key (id_visitante) 
        references funcionarios (id)

    alter table visitas 
        add index (id_tipo_visita), 
        add constraint FK9890A910BD057632 
        foreign key (id_tipo_visita) 
        references tipos_visitas (id)

    alter table visitas 
        add index (id_ordem_servico), 
        add constraint FK9890A91042CBD094 
        foreign key (id_ordem_servico) 
        references ordens_servico (id)

    alter table funcionarios 
        add index (id), 
        add constraint FK1DC35AD37D120B9A 
        foreign key (id) 
        references pessoas (id)

    alter table clientes 
        add index (id), 
        add constraint FKFC4BBD197D120B9A 
        foreign key (id) 
        references pessoas (id)
