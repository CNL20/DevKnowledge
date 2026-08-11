using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevKnowledge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Phase2_DomainStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "topics",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "domain_id",
                table: "topics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "topics",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "topics",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "domains",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "domains",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "domains",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_topics_domain_id_name",
                table: "topics",
                columns: new[] { "domain_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_topics_domain_id_slug",
                table: "topics",
                columns: new[] { "domain_id", "slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_domains_name",
                table: "domains",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_domains_slug",
                table: "domains",
                column: "slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_topics_domains_domain_id",
                table: "topics",
                column: "domain_id",
                principalTable: "domains",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_topics_domains_domain_id",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "ix_topics_domain_id_name",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "ix_topics_domain_id_slug",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "ix_domains_name",
                table: "domains");

            migrationBuilder.DropIndex(
                name: "ix_domains_slug",
                table: "domains");

            migrationBuilder.DropColumn(
                name: "description",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "domain_id",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "name",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "topics");

            migrationBuilder.DropColumn(
                name: "description",
                table: "domains");

            migrationBuilder.DropColumn(
                name: "name",
                table: "domains");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "domains");
        }
    }
}
