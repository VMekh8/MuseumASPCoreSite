﻿namespace MuseumSite.Domain.Entitites
{
    public class ExhitbitEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public Guid? ExhibitionEntityId { get; set; }

        public ExhibitionEntity? ExhibitionEntity { get; set; }
    }
}