import React, { useState } from 'react'
import { TextField, PrimaryButton, DefaultButton } from 'office-ui-fabric-react';
import { Stack, IStackProps, IStackStyles, IStackTokens } from 'office-ui-fabric-react/lib/Stack';
import { ILibraryDto, Address, LibraryDto } from '../../generated/backend';

// Prop interface 
// Allows for setAddMode and handleCreateLibrary methods to be passed in
interface IProps {
    setAddMode: (addMode: boolean) => void;
    handleCreateLibrary: (library: ILibraryDto) => void;
}

// Component for a form to create a new library entry
// Takes in two functions - setAddMode and handleCreateLibrary as props
const LibraryForm: React.FC<IProps> = ({setAddMode, handleCreateLibrary}) => {

    // To initialize Fluent UI stack components 
    const stackStyles: Partial<IStackStyles> = { root: { width: 650 }};
    const columnProps: Partial<IStackProps> = {
        tokens: { childrenGap: 15 },
        styles: { root: { width: 300 }}
    }

    const stackTokens: IStackTokens = { childrenGap: 10 };

    // Initialize the form's values to be empty
    const initializeForm = () => {
        return {
            Id: '',
            Name: '',
            LocationAddressLine1: '',
            LocationAddressLine2: '',
            LocationCity: '',
            LocationStateProvince: '',
            LocationZipCode: '',
            LocationCountry: ''
        }
    }

    // State hooks for the component with library property
    // Library represents the new library that will be created
    const [library, setLibrary] = useState(initializeForm);

    // When user types into text fields, this method will update the values of the respective text fields
    const handleInputChange = (event: any) => {
        //console.log(event.target.value);
        const {name, value} = event.target;
        setLibrary({...library, [name]: value})
    }

    // Function that will handle the submit event when user clicks submit button
    const handleSubmit = async () => {
        //console.log("Submitted");
        //console.log(library);

        // Checks to see if all required fields have been filled out
        if(library.Name !== '' && library.LocationAddressLine1 !== '' && library.LocationCity !== ''
            && library.LocationCountry !== '' && library.LocationZipCode !== '') {

                // Object representing an address
                // Used to create a new Address 
                let addressItem = {
                    locationAddressLine1: library.LocationAddressLine1,
                    locationAddressLine2: library.LocationAddressLine2,
                    locationCity: library.LocationCity,
                    locationStateProvince: library.LocationStateProvince,
                    locationZipCode: library.LocationZipCode,
                    locationCountry: library.LocationCountry
                }
        
                let newAddress = Address.fromJS(addressItem);
                //console.log("New address: ");
                //console.log(newAddress);
                
                // Object that represent a new Library item
                // Used to create a new Library DTO 
                let libraryItem:ILibraryDto = {
                    id: Math.floor(Math.random() * 10000),
                    name: library.Name,
                    address: newAddress,
                    createdDate: new Date(),
                    updatedDate: new Date()
                }
        
                //console.log("Library item " + libraryItem.name);
                
                // Create new Library DTO
                let newLibrary = LibraryDto.fromJS(libraryItem);
        
                //console.log(newLibrary);
                
                // Pass in new Library DTO to handleCreateLibrary function
                // Saves new Library DTO to the database
                handleCreateLibrary(newLibrary);

                // Set add mode to false to hide form
                setAddMode(false);
        } else {

            // Alerts user if required fields have not been filled out
            alert('Please fill out all required fields');
        }

    }

    return(
        <Stack onSubmit={handleSubmit} horizontal tokens={{ childrenGap: 50}} styles={stackStyles}>
            <Stack {... columnProps}>
                <TextField 
                    onChange={handleInputChange} 
                    name="Name" value={library.Name} 
                    label="Library Name" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationAddressLine1" 
                    value={library.LocationAddressLine1} 
                    label="Street Address" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationAddressLine2" 
                    value={library.LocationAddressLine2} 
                    label="Street Address 2" 
                />
                <TextField 
                    onChange={handleInputChange}
                    name="LocationCity" 
                    value={library.LocationCity} 
                    label="City" 
                    required
                />
            </Stack>   

            <Stack {... columnProps}>
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationStateProvince" 
                    value={library.LocationStateProvince} 
                    label="State" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationZipCode" 
                    value={library.LocationZipCode} 
                    label="Zip Code" 
                    required
                />
                <TextField  
                    onChange={handleInputChange}
                    name="LocationCountry" 
                    value={library.LocationCountry} 
                    label="Country" 
                    required
                />
                <Stack horizontal tokens={stackTokens}>
                    <PrimaryButton type="submit" onClick={handleSubmit} text="Submit" />
                    <DefaultButton onClick={() => {setAddMode(false)}} text="Cancel" />
                </Stack>
            </Stack>
        </Stack>
    )

}

export default LibraryForm;