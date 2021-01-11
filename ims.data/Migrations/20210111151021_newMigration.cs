using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ims.data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "action_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_action_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_name = table.Column<string>(nullable: true),
                    table_name = table.Column<string>(nullable: true),
                    user_action = table.Column<long>(nullable: true),
                    time_stamp = table.Column<long>(nullable: false),
                    key_values = table.Column<string>(nullable: true),
                    old_values = table.Column<string>(nullable: true),
                    new_values = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_audit_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    color_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    color_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_colors", x => x.color_id);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    i_d = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    file_i_d = table.Column<int>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    file_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    extentsion = table.Column<string>(nullable: true),
                    file = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_documents", x => x.i_d);
                });

            migrationBuilder.CreateTable(
                name: "factories",
                columns: table => new
                {
                    factory_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    factory_name = table.Column<string>(nullable: false),
                    location_description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_factories", x => x.factory_id);
                });

            migrationBuilder.CreateTable(
                name: "machine_codes",
                columns: table => new
                {
                    machine_code_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    machine_code_name = table.Column<string>(maxLength: 50, nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_machine_codes", x => x.machine_code_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sales_reports",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shop_id = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: true),
                    reference_id = table.Column<int>(nullable: false),
                    reported_by = table.Column<long>(nullable: false),
                    total_number = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_from = table.Column<DateTime>(nullable: false),
                    date_to = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_sales_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sexes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    sex_name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_sexes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shops",
                columns: table => new
                {
                    shop_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shop_name = table.Column<string>(maxLength: 50, nullable: false),
                    shop_location = table.Column<string>(nullable: true),
                    shop_id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shops", x => x.shop_id);
                    table.ForeignKey(
                        name: "f_k_shops_shops_shop_id1",
                        column: x => x.shop_id1,
                        principalTable: "shops",
                        principalColumn: "shop_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    size_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    size_number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_sizes", x => x.size_id);
                });

            migrationBuilder.CreateTable(
                name: "soles",
                columns: table => new
                {
                    sole_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    sole_name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_soles", x => x.sole_id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_stores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    i_d = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    t_i_n = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_suppliers", x => x.i_d);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    username = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    fullname = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    regon = table.Column<long>(nullable: false),
                    employeeid = table.Column<int>(nullable: true),
                    password = table.Column<string>(nullable: false),
                    position = table.Column<string>(nullable: false),
                    phone_no = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_documents",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    workflowid = table.Column<Guid>(nullable: false),
                    filename = table.Column<string>(nullable: true),
                    contentdisposition = table.Column<string>(nullable: true),
                    contenttype = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    length = table.Column<long>(nullable: false),
                    file = table.Column<byte[]>(nullable: true),
                    index = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_workflow_documents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_workflow_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shoe_models",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    model_name = table.Column<string>(nullable: true),
                    machine_code_id = table.Column<int>(nullable: false),
                    sole_id = table.Column<int>(nullable: false),
                    image_url = table.Column<string>(nullable: true),
                    image_thumbnail = table.Column<string>(nullable: true),
                    image_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shoe_models", x => x.id);
                    table.ForeignKey(
                        name: "f_k_shoe_models_machine_codes_machine_code_id",
                        column: x => x.machine_code_id,
                        principalTable: "machine_codes",
                        principalColumn: "machine_code_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoe_models__soles_sole_id",
                        column: x => x.sole_id,
                        principalTable: "soles",
                        principalColumn: "sole_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "factory_to_stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date = table.Column<DateTime>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    initiator_id = table.Column<long>(nullable: false),
                    confirmed_by_id = table.Column<long>(nullable: false),
                    workflow_id = table.Column<Guid>(nullable: false),
                    total_number = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    factory_id = table.Column<int>(nullable: false),
                    fptv = table.Column<string>(nullable: true),
                    f_p_t_v__id = table.Column<int>(nullable: false),
                    fprr = table.Column<string>(nullable: true),
                    f_p_r_r__i_d = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_factory_to_stores", x => x.id);
                    table.ForeignKey(
                        name: "f_k_factory_to_stores__user_confirmed_by_id",
                        column: x => x.confirmed_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_factory_to_stores_factories_factory_id",
                        column: x => x.factory_id,
                        principalTable: "factories",
                        principalColumn: "factory_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_factory_to_stores__user_initiator_id",
                        column: x => x.initiator_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_factory_to_stores__stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_to_stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date = table.Column<DateTime>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    initiator_id = table.Column<long>(nullable: false),
                    confirmed_by_id = table.Column<long>(nullable: false),
                    workflow_id = table.Column<Guid>(nullable: false),
                    total_number = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    suppiler_id = table.Column<int>(nullable: false),
                    invoice_number = table.Column<string>(nullable: true),
                    total_price = table.Column<double>(nullable: false),
                    grn = table.Column<string>(nullable: true),
                    g_r_n__id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_purchase_to_stores", x => x.id);
                    table.ForeignKey(
                        name: "f_k_purchase_to_stores__user_confirmed_by_id",
                        column: x => x.confirmed_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_purchase_to_stores__user_initiator_id",
                        column: x => x.initiator_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_purchase_to_stores__stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_purchase_to_stores__suppliers_suppiler_id",
                        column: x => x.suppiler_id,
                        principalTable: "suppliers",
                        principalColumn: "i_d",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shop_to_stores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date = table.Column<DateTime>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    initiator_id = table.Column<long>(nullable: false),
                    confirmed_by_id = table.Column<long>(nullable: false),
                    workflow_id = table.Column<Guid>(nullable: false),
                    total_number = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    shop_id = table.Column<int>(nullable: false),
                    mrn = table.Column<string>(nullable: true),
                    m_r_n__id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shop_to_stores", x => x.id);
                    table.ForeignKey(
                        name: "f_k_shop_to_stores__user_confirmed_by_id",
                        column: x => x.confirmed_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shop_to_stores__user_initiator_id",
                        column: x => x.initiator_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shop_to_stores_shops_shop_id",
                        column: x => x.shop_id,
                        principalTable: "shops",
                        principalColumn: "shop_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shop_to_stores__stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "store_to_shops",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date = table.Column<DateTime>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    initiator_id = table.Column<long>(nullable: false),
                    confirmed_by_id = table.Column<long>(nullable: false),
                    workflow_id = table.Column<Guid>(nullable: false),
                    total_number = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    shop_id = table.Column<int>(nullable: false),
                    dn = table.Column<string>(nullable: true),
                    d_n__id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_store_to_shops", x => x.id);
                    table.ForeignKey(
                        name: "f_k_store_to_shops__user_confirmed_by_id",
                        column: x => x.confirmed_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_store_to_shops__user_initiator_id",
                        column: x => x.initiator_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_store_to_shops_shops_shop_id",
                        column: x => x.shop_id,
                        principalTable: "shops",
                        principalColumn: "shop_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_store_to_shops_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_action",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    timestamp = table.Column<long>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    actiontypeid = table.Column<int>(nullable: false),
                    remark = table.Column<string>(nullable: true),
                    usernamenavigationid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user_action", x => x.id);
                    table.ForeignKey(
                        name: "f_k_user_action_action_type_actiontypeid",
                        column: x => x.actiontypeid,
                        principalTable: "action_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_user_action_user_usernamenavigationid",
                        column: x => x.usernamenavigationid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shoes",
                columns: table => new
                {
                    shoe_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shoe_model_id = table.Column<int>(nullable: false),
                    color_id = table.Column<int>(nullable: false),
                    size_id = table.Column<int>(nullable: false),
                    entry_date = table.Column<DateTime>(nullable: false),
                    image_url = table.Column<string>(nullable: true),
                    image_thumbnail = table.Column<string>(nullable: true),
                    sex_id = table.Column<int>(nullable: false),
                    model_unique = table.Column<string>(nullable: true),
                    machine_code_id = table.Column<int>(nullable: true),
                    sole_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shoes", x => x.shoe_id);
                    table.ForeignKey(
                        name: "f_k_shoes_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "color_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoes_machine_codes_machine_code_id",
                        column: x => x.machine_code_id,
                        principalTable: "machine_codes",
                        principalColumn: "machine_code_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_shoes_sexes_sex_id",
                        column: x => x.sex_id,
                        principalTable: "sexes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoes__shoe_models_shoe_model_id",
                        column: x => x.shoe_model_id,
                        principalTable: "shoe_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoes__sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "size_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoes__soles_sole_id",
                        column: x => x.sole_id,
                        principalTable: "soles",
                        principalColumn: "sole_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userid = table.Column<long>(nullable: false),
                    roleid = table.Column<long>(nullable: false),
                    aid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user_role", x => x.id);
                    table.ForeignKey(
                        name: "f_k_user_role_user_action_aid",
                        column: x => x.aid,
                        principalTable: "user_action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_user_role_role_roleid",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_user_role_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    currentstate = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    typeid = table.Column<int>(nullable: false),
                    aid = table.Column<long>(nullable: true),
                    initiatoruser = table.Column<string>(nullable: true),
                    employeeid = table.Column<int>(nullable: true),
                    observer = table.Column<string>(nullable: true),
                    parentwfid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_workflow", x => x.id);
                    table.ForeignKey(
                        name: "f_k_workflow_user_action_aid",
                        column: x => x.aid,
                        principalTable: "user_action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_workflow__workflow_type_typeid",
                        column: x => x.typeid,
                        principalTable: "workflow_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shoe_transaction_lists",
                columns: table => new
                {
                    i_d = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shoe_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    unit_price = table.Column<double>(nullable: true),
                    transaction_type = table.Column<int>(nullable: false),
                    transaction_id = table.Column<int>(nullable: false),
                    factory_to_store_id = table.Column<int>(nullable: true),
                    purchase_to_store_id = table.Column<int>(nullable: true),
                    shop_to_store_id = table.Column<int>(nullable: true),
                    store_to_shop_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shoe_transaction_lists", x => x.i_d);
                    table.ForeignKey(
                        name: "f_k_shoe_transaction_lists_factory_to_stores_factory_to_store_id",
                        column: x => x.factory_to_store_id,
                        principalTable: "factory_to_stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_shoe_transaction_lists_purchase_to_stores_purchase_to_store~",
                        column: x => x.purchase_to_store_id,
                        principalTable: "purchase_to_stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_shoe_transaction_lists_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "shoe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shoe_transaction_lists__shop_to_stores_shop_to_store_id",
                        column: x => x.shop_to_store_id,
                        principalTable: "shop_to_stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_shoe_transaction_lists__store_to_shops_store_to_shop_id",
                        column: x => x.store_to_shop_id,
                        principalTable: "store_to_shops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shop_stocks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shoe_id = table.Column<int>(nullable: false),
                    shop_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    seq_no = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_shop_stocks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_shop_stocks_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "shoe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_shop_stocks_shops_shop_id",
                        column: x => x.shop_id,
                        principalTable: "shops",
                        principalColumn: "shop_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    shoe_id = table.Column<int>(nullable: false),
                    size_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_stocks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_stocks_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "shoe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_stocks_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "size_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "store_stocks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    store_id = table.Column<int>(nullable: false),
                    shoe_id = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    seq_no = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_store_stocks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_store_stocks_shoes_shoe_id",
                        column: x => x.shoe_id,
                        principalTable: "shoes",
                        principalColumn: "shoe_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_store_stocks_stores_store_id",
                        column: x => x.store_id,
                        principalTable: "stores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_item",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    workflow_id = table.Column<Guid>(nullable: false),
                    seqno = table.Column<int>(nullable: false),
                    fromstate = table.Column<int>(nullable: false),
                    trigger = table.Column<int>(nullable: false),
                    datatype = table.Column<string>(nullable: true),
                    data = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    assignedrole = table.Column<long>(nullable: true),
                    assigneduser = table.Column<long>(nullable: true),
                    aid = table.Column<long>(nullable: true),
                    assignedrolenavigationid = table.Column<long>(nullable: true),
                    assignedusernavigationid = table.Column<long>(nullable: true),
                    assignedusernaviagtion_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_work_item", x => x.id);
                    table.ForeignKey(
                        name: "f_k_work_item_user_action_aid",
                        column: x => x.aid,
                        principalTable: "user_action",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_work_item_role_assignedrolenavigationid",
                        column: x => x.assignedrolenavigationid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_work_item_user_assignedusernaviagtion_id",
                        column: x => x.assignedusernaviagtion_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_work_item__workflow_workflow_id",
                        column: x => x.workflow_id,
                        principalTable: "workflow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_factory_to_stores_confirmed_by_id",
                table: "factory_to_stores",
                column: "confirmed_by_id");

            migrationBuilder.CreateIndex(
                name: "i_x_factory_to_stores_factory_id",
                table: "factory_to_stores",
                column: "factory_id");

            migrationBuilder.CreateIndex(
                name: "i_x_factory_to_stores_initiator_id",
                table: "factory_to_stores",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "i_x_factory_to_stores_store_id",
                table: "factory_to_stores",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_to_stores_confirmed_by_id",
                table: "purchase_to_stores",
                column: "confirmed_by_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_to_stores_initiator_id",
                table: "purchase_to_stores",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_to_stores_store_id",
                table: "purchase_to_stores",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_purchase_to_stores_suppiler_id",
                table: "purchase_to_stores",
                column: "suppiler_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_models_machine_code_id",
                table: "shoe_models",
                column: "machine_code_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_models_sole_id",
                table: "shoe_models",
                column: "sole_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_transaction_lists_factory_to_store_id",
                table: "shoe_transaction_lists",
                column: "factory_to_store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_transaction_lists_purchase_to_store_id",
                table: "shoe_transaction_lists",
                column: "purchase_to_store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_transaction_lists_shoe_id",
                table: "shoe_transaction_lists",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_transaction_lists_shop_to_store_id",
                table: "shoe_transaction_lists",
                column: "shop_to_store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoe_transaction_lists_store_to_shop_id",
                table: "shoe_transaction_lists",
                column: "store_to_shop_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_color_id",
                table: "shoes",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_machine_code_id",
                table: "shoes",
                column: "machine_code_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_sex_id",
                table: "shoes",
                column: "sex_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_shoe_model_id",
                table: "shoes",
                column: "shoe_model_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_size_id",
                table: "shoes",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shoes_sole_id",
                table: "shoes",
                column: "sole_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_stocks_shoe_id",
                table: "shop_stocks",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_stocks_shop_id",
                table: "shop_stocks",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_to_stores_confirmed_by_id",
                table: "shop_to_stores",
                column: "confirmed_by_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_to_stores_initiator_id",
                table: "shop_to_stores",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_to_stores_shop_id",
                table: "shop_to_stores",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shop_to_stores_store_id",
                table: "shop_to_stores",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_shops_shop_id1",
                table: "shops",
                column: "shop_id1");

            migrationBuilder.CreateIndex(
                name: "i_x_stocks_shoe_id",
                table: "stocks",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "i_x_stocks_size_id",
                table: "stocks",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_stocks_shoe_id",
                table: "store_stocks",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_stocks_store_id",
                table: "store_stocks",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_to_shops_confirmed_by_id",
                table: "store_to_shops",
                column: "confirmed_by_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_to_shops_initiator_id",
                table: "store_to_shops",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_to_shops_shop_id",
                table: "store_to_shops",
                column: "shop_id");

            migrationBuilder.CreateIndex(
                name: "i_x_store_to_shops_store_id",
                table: "store_to_shops",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "i_x_user_action_actiontypeid",
                table: "user_action",
                column: "actiontypeid");

            migrationBuilder.CreateIndex(
                name: "i_x_user_action_usernamenavigationid",
                table: "user_action",
                column: "usernamenavigationid");

            migrationBuilder.CreateIndex(
                name: "i_x_user_role_aid",
                table: "user_role",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "i_x_user_role_roleid",
                table: "user_role",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "i_x_user_role_userid",
                table: "user_role",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "i_x_work_item_aid",
                table: "work_item",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "i_x_work_item_assignedrolenavigationid",
                table: "work_item",
                column: "assignedrolenavigationid");

            migrationBuilder.CreateIndex(
                name: "i_x_work_item_assignedusernaviagtion_id",
                table: "work_item",
                column: "assignedusernaviagtion_id");

            migrationBuilder.CreateIndex(
                name: "i_x_work_item_workflow_id",
                table: "work_item",
                column: "workflow_id");

            migrationBuilder.CreateIndex(
                name: "i_x_workflow_aid",
                table: "workflow",
                column: "aid");

            migrationBuilder.CreateIndex(
                name: "i_x_workflow_typeid",
                table: "workflow",
                column: "typeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "sales_reports");

            migrationBuilder.DropTable(
                name: "shoe_transaction_lists");

            migrationBuilder.DropTable(
                name: "shop_stocks");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "store_stocks");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "work_item");

            migrationBuilder.DropTable(
                name: "workflow_documents");

            migrationBuilder.DropTable(
                name: "factory_to_stores");

            migrationBuilder.DropTable(
                name: "purchase_to_stores");

            migrationBuilder.DropTable(
                name: "shop_to_stores");

            migrationBuilder.DropTable(
                name: "store_to_shops");

            migrationBuilder.DropTable(
                name: "shoes");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "workflow");

            migrationBuilder.DropTable(
                name: "factories");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "shops");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "sexes");

            migrationBuilder.DropTable(
                name: "shoe_models");

            migrationBuilder.DropTable(
                name: "sizes");

            migrationBuilder.DropTable(
                name: "user_action");

            migrationBuilder.DropTable(
                name: "workflow_type");

            migrationBuilder.DropTable(
                name: "machine_codes");

            migrationBuilder.DropTable(
                name: "soles");

            migrationBuilder.DropTable(
                name: "action_type");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
