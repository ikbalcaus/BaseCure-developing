﻿using BaseCureAPI.DB;
using BaseCureAPI.DB.Models;
using BaseCureAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseCureAPI.Endpoints.Korisnik
{
    [Route("korisnici")]
    [ApiController]
    public class KorisniciEndpoints : ControllerBase
    {

        private readonly BaseCureContext _context;

        public KorisniciEndpoints(BaseCureContext context)
        {
            _context = context;
        }

        // GET: korisnici
        [HttpGet]
        public ActionResult<KorisniciGetAllResponse> Get()
        {
            var korisnik = _context.Korisnicis.OrderByDescending(x => x.KorisnikId)
                .Select(x => new KorisniciRes()
                {
                    KorisnikId = x.KorisnikId,
                    KorisnickoIme = x.KorisnickoIme,
                    HashLozinke = x.HashLozinke,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    DatumRodjenja = (DateTime)x.DatumRodjenja,
                    MailAdresa = x.MailAdresa,
                    Uloga = x.Uloga,
                }).ToList();

            return new KorisniciGetAllResponse
            {
                Korisnici = korisnik
            };
        }

        // GET korisnici/5
        [HttpGet("{id}")]
        public ActionResult<KorisniciRes> GetKorisnik(int id)
        {

            var korisnik = _context.Korisnicis.OrderByDescending(x => x.KorisnikId)
                .Select(x => new KorisniciRes()
                {
                    KorisnikId = x.KorisnikId,
                    KorisnickoIme = x.KorisnickoIme,
                    HashLozinke = x.HashLozinke,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    DatumRodjenja = (DateTime)x.DatumRodjenja,
                    MailAdresa = x.MailAdresa,
                    Uloga = x.Uloga,
                }).Single(x=>x.KorisnikId == id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return korisnik;
        }

        [HttpPost]
        public ActionResult<int> CreateKorisnik([FromBody] KorisniciReq req)
        {
            Korisnici? korisnik;

            if (req.ID == 0)
            {
                korisnik = new Korisnici();
                _context.Add(korisnik);
            }
            else
            {
                korisnik = _context.Korisnicis.FirstOrDefault(x => x.KorisnikId == req.ID);
                if (korisnik == null)
                    throw new Exception("wrong ID");
            }

            korisnik.Ime = req.Ime.RemoveTags();
            korisnik.Prezime = req.Prezime.RemoveTags();

            _context.SaveChanges();

            return korisnik.KorisnikId;
        }

        // PUT: korisnici/5
        [HttpPut("{id}")]
        public IActionResult UpdateKorisnik(int id, [FromBody] KorisniciRes korisnik)
        {
            if (id != korisnik.KorisnikId)
            {
                return BadRequest();
            }

            _context.Entry(korisnik).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: korisnici/5
        [HttpDelete("{id}")]
        public IActionResult DeleteKorisnik(int id)
        {
            var korisnik = _context.Korisnicis.Find(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            _context.Korisnicis.Remove(korisnik);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
