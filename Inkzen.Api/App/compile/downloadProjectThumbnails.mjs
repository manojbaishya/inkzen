import { writeFile } from 'node:fs/promises';
import { Readable } from 'node:stream';
import 'dotenv/config';

import { extractFilename, ProjectImageWidth } from '../utils/images.mjs';



/**
 * Thumbnails array
 *
 * @type {string[]}
 */
const thumbnails = [];

/**
 * Project Ids array
 *
 * @type {string[]}
 */
const project_ids = [];

const getAllProjects = `${process.env.VITE_BASEURL}/${process.env.VITE_API_GETALLPROJECTS}`;
try {
    const response = await fetch(getAllProjects);

    const projects = await response.json();

    for (const project of projects) {
        thumbnails.push(project.primaryImage.media.publicUrl.replace('~', process.env.VITE_SERVICE));
        project_ids.push(project.id);
    }

} catch (err){
    console.log(`ERROR: ${err}`);
}

//#region Download thumbnails
console.log("Downloading Project Thumbnails..");
for (const url of thumbnails) {
    try {
        const response = await fetch(url);
        const body = Readable.fromWeb(response.body);

        const filename = extractFilename(url);
        await writeFile(`public/Projects/${filename}`, body);
        console.log(`Successfully saved ${filename} to the public/ directory`);
    } catch(err) {
        console.log(`ERROR: ${err}`);
    }
}
console.log("***********************************");
//#endregion


//#region Download each project pictures
console.log("Downloading Project Details Images..");
for (const projectId of project_ids) {
    const getProject = `${process.env.VITE_BASEURL}/${process.env.VITE_API_GETPROJECT}/${projectId}?imageSize=${ProjectImageWidth}`;

    try {
        const response = await fetch(getProject);

        const project = await response.json();

        /**
         * @type {{id: string, src: string, alt: string}[]}
         */
        const project_images = project.gallery.map(img => ({
            id: img.id,
            src: img.url.replace('~', process.env.VITE_SERVICE),
            alt: img.altText
        }));

        for (const img of project_images) {
            const url = img.src;
            try {
                const response = await fetch(url);
                const body = Readable.fromWeb(response.body);

                const filename = extractFilename(url);
                await writeFile(`public/Projects/${filename}`, body);
                console.log(`Successfully saved ${filename} to the public/ directory`);
            } catch(err) {
                console.log(`ERROR: ${err}`);
            }
        }
    } catch (err){
        console.log(`ERROR: ${err}`);
    }
}
console.log("***********************************");
//#endregion

//#region Download team members
/**
 * Members array
 *
 * @type {string[]}
 */
const members = [];

const getAllTeam = `${process.env.VITE_BASEURL}/${process.env.VITE_API_GETALLTEAM}`;

try {
    const response = await fetch(getAllTeam);

    const team = await response.json();

    for (const member of team) {
        members.push(member.photo.media.publicUrl.replace('~', process.env.VITE_SERVICE));
    }

} catch (err){
    console.log(`ERROR: ${err}`);
}

console.log("Downloading Team Members...");
for (const url of members) {
    try {
        const response = await fetch(url);
        const body = Readable.fromWeb(response.body);

        const filename = extractFilename(url);
        await writeFile(`public/About/${filename}`, body);
        console.log(`Successfully saved ${filename} to the public/ directory`);
    } catch(err) {
        console.log(`ERROR: ${err}`);
    }
}
console.log("***********************************");
//#endregion